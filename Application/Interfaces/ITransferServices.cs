using Application.Request;
using Application.Response;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransferServices
    {
        Task<List<TransferResponse>> GetAll();
        Task<List<TransferResponse>> GetAllByUser(Guid id);
        Task<TransferResponse> CreateTransfer(CreateTransferRequest request);
        Task<TransferResponse> UpdateTransfer(UpdateTransferRequest request, Guid transferId);
        Task<TransferResponse> DeleteTransfer(Guid transfer);
    }
}
