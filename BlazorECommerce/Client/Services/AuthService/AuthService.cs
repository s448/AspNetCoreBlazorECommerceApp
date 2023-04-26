﻿
using BlazorEcommerce.Shared;

namespace BlazorECommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", userChangePassword.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
