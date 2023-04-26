using BlazorECommerce.Shared;
using BlazorECommerce.Shared.DTO;
using Blazored.LocalStorage;

namespace BlazorECommerce.Client.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider authStateProvider;

        public CardService(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            this.authStateProvider = authStateProvider;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }


        public event Action OnChange;

        public async Task AddToCart(CardItem cardItem)
        {
            if (await IsUserAuthenticated())
            {
                await _httpClient.PostAsJsonAsync("api/cart/add", cardItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
                if (cart == null)
                {
                    cart = new List<CardItem>();
                }
                var sameItem = cart.Find(i => i.ProductId == cardItem.ProductId && i.ProductTypeId == cardItem.ProductTypeId);
                if (sameItem == null)
                {
                    cart.Add(cardItem);
                }
                else
                {
                    sameItem.Quantity += cardItem.Quantity;
                }
                await _localStorage.SetItemAsync("cart", cart);
            }



            await GetCartItemsCount();
        }


        public async Task<List<CardItem>> GetAllCartItems()
        {
            await GetCartItemsCount();
            var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
            if (cart == null)
            {
                cart = new List<CardItem>();
            }
            return cart;
        }
        /*
         * [
         * {
         *  productId,
         *  ProductTypeId,
         * },
         * {
         *  productId,
         *  ProductTypeId,
         * },
         * {
         *  productId,
         *  ProductTypeId,
         * }
         * ]
         */

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            if (await IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CardItem>>("cart");
                if (cartItems == null)
                    return new List<CartProductResponse>();
                var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
                return cartProducts.Data;
            }

        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            if (await IsUserAuthenticated())
            {
                await _httpClient.DeleteAsync($"api/cart/{productId}/{productTypeId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
                if (cart == null)
                    return;
                var cartItem = cart.Find(p => p.ProductId == productId && p.ProductTypeId == productTypeId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task StoreCartItems(bool emptyFlag = false)
        {
            var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
            if (cart == null)
                return;
            await _httpClient.PostAsJsonAsync("api/cart", cart);
            if (emptyFlag)
                await _localStorage.RemoveItemAsync("cart");
        }

        public async Task UpdateCartItemQuantity(CartProductResponse cartProductResponse)
        {
            if (await IsUserAuthenticated())
            {
                var request = new CardItem
                {
                    ProductId = cartProductResponse.ProductId,
                    Quantity = cartProductResponse.Quantity,
                    ProductTypeId = cartProductResponse.ProductTypeId
                };
                await _httpClient.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {

                var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
                if (cart == null)
                    return;
                var cartItem = cart.Find(p => p.ProductId == cartProductResponse.ProductId &&
                p.ProductTypeId == cartProductResponse.ProductTypeId);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartProductResponse.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task GetCartItemsCount()
        {
            if (await IsUserAuthenticated())
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;

                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                var cardItems = await _localStorage.GetItemAsync<List<CardItem>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cardItems != null ? cardItems.Count : 0);
            }

            OnChange.Invoke();
        }
    }
}
