using Application.Interfaces;
using Application.Mappers.IMappers;
using Application.Request;
using Application.Response;
using Domain.Models;

namespace Application.UseCases
{
    public class TransferServices : ITransferServices
    {
        private readonly ITransferCommand _command;
        private readonly ITransferQuery _query;
        private readonly ITransferMapper _mapper;
        private readonly IAccountHttpService _accountHttpService;

        public TransferServices(ITransferCommand command, ITransferQuery query, ITransferMapper mapper,IAccountHttpService accountHttpService)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _accountHttpService = accountHttpService;
        }
        public async Task<TransferResponse> CreateTransfer(CreateTransferRequest request)
        {
            var user1 = await _accountHttpService.GetAccountById(request.SrcAccountId)
                ?? throw new InvalidOperationException("User not found");
            var user2 = await _accountHttpService.GetAccountById(request.DestAccountId)
                ?? throw new InvalidOperationException("User not found");


            var transfer = new Transfer
            {
                Amount = request.Amount,
                Date = request.Date,
                StatusId =1,
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = request.DestAccountId,
            };
            var response = await _command.InsertTransfer(transfer);

            return await _mapper.GetOneTransfer(await _query.GetTransferById(response.Id));
        }

        public async Task<TransferResponse> UpdateTransfer(UpdateTransferRequest request, Guid transferId)
        {
            var tranfer = await _query.GetTransferById(transferId);
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                tranfer.Description = request.Description;
            }
            await _command.UpdateTransfer(tranfer);
            
            return await _mapper.GetOneTransfer(await _query.GetTransferById(tranfer.Id));
        }

        public async Task<TransferResponse> DeleteTransfer(Guid transfer)
        {
            var response = await _query.GetTransferById(transfer);
            await _command.DeleteTransfer(transfer);

            return await _mapper.GetOneTransfer(response);
        }

        public async Task<List<TransferResponse>> GetAll()
        {
            var transfers = await _query.GetAll();
            return await _mapper.GetTransfers(transfers);
        }

        public async Task<List<TransferResponse>> GetAllByUser(Guid UserId)
        {
            var userTransfers = await _query.GetUserTransfers(UserId);
            return await _mapper.GetTransfers(userTransfers);
        }


    }
}
