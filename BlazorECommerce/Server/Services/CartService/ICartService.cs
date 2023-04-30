using BlazorECommerce.Shared.DTO;

namespace BlazorECommerce.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponse>>> GetCartItems(List<CardItem> cartItems);
        Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CardItem> cardItems);
        Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(int? userId = null);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<bool>> AddToCart(CardItem cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(CardItem cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
    }
}
