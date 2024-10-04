using Application.Request;
using Application.Response;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITransferServices
    {
        Task<List<TransferResponse>> GetAll();
        Task<List<TransferResponse>> GetAllByUser(Guid id); //La idea de esto seria que, cuando uno consulte todas sus tranferencias, se le pase el dato automaticamente????
        Task<TransferResponse> CreateTransfer(CreateTransferRequest request);
        Task<bool> MakeTransfer(Transfer transfer, AccountModel srcAccount, AccountModel destAccount);
        Task<TransferResponse> UpdateTransfer(CreateTransferRequest transfer);
        Task<TransferResponse> DeleteTransfer(Transfer transfer);
    }
}
