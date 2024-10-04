using Application.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class TransferCommand : ITransferCommand
    {
        private readonly TransferContext _context;
        public TransferCommand(TransferContext context)
        {
            _context = context;
        }

        public async Task CompleteTransfer(AccountModel srcAccount, AccountModel destAccount)
        {
            //Este metodo tendria que ir en AccountCommand
            _context.Update(srcAccount);
            _context.Update(destAccount);
            await _context.SaveChangesAsync();
            //return true;
            //throw new NotImplementedException();
        }

        public async Task DeleteTransfer(Transfer transfer)
        {
            _context.Remove(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertTransfer(Transfer transfer)
        {
            _context.Add(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransfer(Transfer transfer)
        {
            _context.Update(transfer);
            await _context.SaveChangesAsync();
        }
    }
}
