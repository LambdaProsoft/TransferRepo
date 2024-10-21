﻿using Application.Interfaces;
using Application.Request;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            var response = await _httpClient.GetAsync($"https://localhost:7214/api/Account/{id}");
            if (response.IsSuccessStatusCode)
            {
                var respo = response;

                return await response.Content.ReadFromJsonAsync<AccountDetailsResponse>();
            }

            return null; // Manejar errores de forma apropiada
        }

        public async Task<TransferProcess> UpdateAccountBalance(Guid id, AccountBalanceRequest balanceData)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(balanceData),
                Encoding.UTF8,
                "application/json"
                );
            var response = await _httpClient.PatchAsync($"https://localhost:7214/api/Account/Update/Balance/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TransferProcess>();
            }

            return null;
        }


        public async Task<AccountResponse> GetAccountByUserId(Guid userId)
        {

            var response = await _httpClient.GetAsync($"https://localhost:7214/api/Account/User/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var respo = response;

                return await response.Content.ReadFromJsonAsync<AccountResponse>();
            }

            return null; // Manejar errores de forma apropiada
        }

    }
}
