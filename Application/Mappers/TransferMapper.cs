using Application.Mappers.IMappers;
using Application.Response;
using Domain.Models;
using System.Transactions;

namespace Application.Mappers
{
    public class TransferMapper : ITransferMapper
    {
        public TransferMapper() { }
        public async Task<TransferResponse> GetOneTransfer(Transfer transfer)
        {
            var response = new TransferResponse
            {
                Id = transfer.Id,
                Amount = transfer.Amount,
                Date = transfer.Date,
                Status = await GetTransferStatusResponse(transfer.Status),
                Description = transfer.Description,
                TypeId = transfer.TypeId,
                SrcAccountId = transfer.SrcAccountId,
                DestAccountId = transfer.DestAccountId,
            };
            return response;
        }

        public async Task<List<TransferResponse>> GetTransfers(List<Transfer> transfers)
        {
            List<TransferResponse> responses = new List<TransferResponse>();
            foreach (var transfer in transfers)
            {
                var response = new TransferResponse
                {
                    Id = transfer.Id,
                    Amount = transfer.Amount,
                    Date = transfer.Date,
                    Status = await GetTransferStatusResponse(transfer.Status),
                    Description = transfer.Description,
                    TypeId = transfer.TypeId,
                    SrcAccountId = transfer.SrcAccountId,
                    DestAccountId = transfer.DestAccountId,
                };
                responses.Add(response);
            }
            return responses;
        }

        public Task<TransferStatusResponse> GetTransferStatusResponse(TransferStatus status)
        {
            var response = new TransferStatusResponse
            {
                Id = status.TransferStatusId,
                Status = status.Status,
            };
            return Task.FromResult(response) ;
         }
    }
}
