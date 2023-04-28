using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.WebRequestMethods;

namespace BlazorECommerce.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;
        public OrderService(AuthenticationStateProvider authStateProvider, NavigationManager navigationManager, HttpClient httpClient)
        {
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
            _httpClient = httpClient;
        }
        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
            return result.Data;
        }

        public async Task<string> PlaceOrder()
        {
            if (await IsUserAuthenticated())
            {
                var result = await _httpClient.PostAsync("api/payment/checkout", null);
                var url = await result.Content.ReadAsStringAsync();
                return url;
            }
            else
            {
                return "login";
            }
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
            return result.Data;
        }
    }
}
