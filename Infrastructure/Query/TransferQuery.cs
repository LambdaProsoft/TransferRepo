using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class TransferQuery : ITransferQuery
    {
        private readonly TransferContext _context;
        public TransferQuery(TransferContext context)
        {
            _context = context;
        }

        public async Task<List<Transfer>> GetAll()
        {
            var transfers = await _context.Transfers.ToListAsync();
            return transfers;
        }

        public async Task<bool> GetPendingTransfer(Guid UserId)
        {
            var status = false;

            //var transfer = _context.Transfers.Where(t => t.Status == "Pending")
            //    .FirstOrDefault(t => t.SrcAccountId == UserId);

            //if (transfer != null)
            //{
            //    status = true;
            //}
            return status;
        }

        public async Task<List<Transfer>> GetTransferByAlias(string alias)
        {
            throw new NotImplementedException();
            //Esto tendria que buscar en la base de datos de Account, o que se convoque (En el TransferService)
            //un metodo de AccountQuery que busque por alias
        }

        public async Task<List<Transfer>> GetTransferByFilter(Guid? UserId, decimal? amount, DateTime? date, int? type, string? status)//Añadir Alias tambien
        {
            throw new NotImplementedException();
        }

        public async Task<Transfer> GetTransferById(Guid Id)
        {
            var project = _context.Transfers
                .Include(t => t.TransferType)
                .Include(t => t.Status)
                .FirstOrDefault(s => s.Id == Id);
            return project;
        }
        public async Task<List<Transfer>> GetUserTransfers(Guid UserId)
        {
            return await _context.Transfers.Where(t => t.SrcAccountId == UserId).ToListAsync();
        }
    }
}
