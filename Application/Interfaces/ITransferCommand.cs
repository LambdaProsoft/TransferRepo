using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransferCommand
    {
        public Task InsertTransfer(Transfer transfer);

        //Este seria cambiado por un metodo para actualizar Accounts en Account
        public Task CompleteTransfer(AccountModel srcAccount, AccountModel destAccount);
        public Task DeleteTransfer(Transfer transfer);
        public Task UpdateTransfer(Transfer transfer);
    }
}
