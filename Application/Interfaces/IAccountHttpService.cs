using Application.Request;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountHttpService
    {
        Task<AccountDetailsResponse> GetAccountById(Guid id);
        Task<TransferProcess> UpdateAccountBalance(Guid id, AccountBalanceRequest balanceData);
        Task<AccountResponse> GetAccountByUserId(Guid userId);
    }
}
