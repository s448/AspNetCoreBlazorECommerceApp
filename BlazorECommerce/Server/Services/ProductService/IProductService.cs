using BlazorECommerce.Shared.DTO;

namespace BlazorECommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProduct(string searchText, int page);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
        Task<ServiceResponse<List<string>>> GetSearchSuggestions(string searchText);
    }
}
