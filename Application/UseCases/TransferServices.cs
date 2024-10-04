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
        // private readonly IAccountQuery _accountQuery;
        //private readonly IAccountCommand _accountCommand;
        //Necesitaria acceder a la Cuenta que realiza la transferencia y verificar en la lista de transferencias,
        //si hay alguna a su nombre que este pendiente

        public TransferServices(ITransferCommand command, ITransferQuery query, ITransferMapper mapper
            //, IAccountQuery accountQuery, IAccountCommand accountCommand
            )
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            //_accountQuery = accountQuery;
            //_accountCommand = accountCommand;
        }
        public async Task<TransferResponse> CreateTransfer(CreateTransferRequest request)
        {
            //var srcAccount = await _accountQuery.GetAccount(request.SrcAccountId);
            //var destAccount = await _accountQuery.GetAccount(request.DestAccountId);

            //var srcAccount = await ConexionAccount.getAccount();
            //var destAccount = await ConexionAccount.getAccount();


            //if (srcAccount == null || destAccount == null) {
            //    throw new ObjectNotFoundException("Account not Found Exception");
            //}

            var transfer = new Transfer
            {
                Amount = request.Amount,
                Date = request.Date,
                Status = "Pending",
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = request.DestAccountId,
            };
            await _command.InsertTransfer(transfer);

            //if (srcAccount.Balance < transfer.Amount)
            //{
            //    throw new Exception("Not enough balance to make transference");
            //}

            //if (await MakeTransfer(transfer, srcAccount, destAccount) == false)
            //{
            //    transfer.Status = "Canceled";
            //    await _command.UpdateTransfer(transfer);
            //}
            //else
            //{
            transfer.Status = "Completed";
            await _command.UpdateTransfer(transfer);
            //}
            return await _mapper.GetOneTransfer(transfer);
        }
        public async Task<bool> MakeTransfer(Transfer transfer, AccountModel srcAccount, AccountModel destAccount)
        {
            if (await _query.GetPendingTransfer(transfer.SrcAccountId) == false)
            {

                if (srcAccount.StateAccount.Name == "Suspended")
                {
                    return false;
                }
                else if (destAccount.StateAccount.Name == "Suspended")
                {
                    return false;
                }
                else if (srcAccount.StateAccount.Name == "Blocked")
                {
                    return false;
                }
                else if (destAccount.StateAccount.Name == "Blocked")
                {
                    return false;
                }

                //srcAccount.Balance -= transfer.Amount;
                //destAccount.Balance += transfer.Amount;

                //LO DE CAMBIAR EL MONTO LO HACE ACCOUNT
                /*CONEXIONACCOUNT.updateBalance(transfer.amount);
                 
                */

                //await _accountCommand.UpdateAccount(srcAccount);
                //await _accountCommand.UpdateAccount(destAccount);



                return true;
            }
            return false;
        }
        public async Task<TransferResponse> UpdateTransfer(CreateTransferRequest request)
        {
            var transfer = new Transfer
            {
                Amount = request.Amount,
                Date = request.Date,
                Status = "",
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = request.DestAccountId,
            };
            await _command.UpdateTransfer(transfer);
            return await _mapper.GetOneTransfer(transfer);
            throw new NotImplementedException();
        }

        public Task<TransferResponse> DeleteTransfer(Transfer transfer)
        {
            throw new NotImplementedException();
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
