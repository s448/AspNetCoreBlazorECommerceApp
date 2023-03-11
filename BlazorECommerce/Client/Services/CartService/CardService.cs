using BlazorECommerce.Shared;
using BlazorECommerce.Shared.DTO;
using Blazored.LocalStorage;

namespace BlazorECommerce.Client.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CardService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        public event Action OnChange;

        public async Task AddToCart(CardItem cardItem)
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

            OnChange?.Invoke();
        }

        public async Task<List<CardItem>> GetAllCartItems()
        {
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
            var cartItems = await _localStorage.GetItemAsync<List<CardItem>>("cart");
            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return cartProducts.Data;
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CardItem>>("cart");
            if (cart == null)
                return;
            var cartItem = cart.Find(p => p.ProductId == productId && p.ProductTypeId == productTypeId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateCartItemQuantity(CartProductResponse cartProductResponse)
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
}
