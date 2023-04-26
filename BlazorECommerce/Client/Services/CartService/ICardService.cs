using BlazorECommerce.Shared.DTO;

namespace BlazorECommerce.Client.Services.CardService
{
    public interface ICardService
    {
        event Action OnChange;
        Task AddToCart(CardItem cardItem);
        //Task<List<CardItem>> GetAllCartItems();
        Task<List<CartProductResponse>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int productTypeId);
        Task UpdateCartItemQuantity(CartProductResponse cartProductResponse);
        Task StoreCartItems(bool emptyFlag);
        Task GetCartItemsCount();
    }
}
