using Application.Interfaces;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AccountHttpClientService: IAccountHttpService
    {
        private readonly HttpClient _httpClient;

        public AccountHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<AccountDetailsResponse> GetAccountById(Guid id)
        {

            var response = await _httpClient.GetAsync($"https://localhost:7160/api/User/1");
            var x = 1;
            if (response.IsSuccessStatusCode)
            {
                var respo = response;

                return await response.Content.ReadFromJsonAsync<AccountDetailsResponse>();
            }

            return null; // Manejar errores de forma apropiada
        }


    }
}
