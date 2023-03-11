using BlazorECommerce.Shared.DTO;

namespace BlazorECommerce.Client.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient _httpClient;
        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; }

        //start => pagination params
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;
        //end => pagination params

        public async Task<ServiceResponse<Product>> GetProductById(int productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl)
        {
            var result = categoryUrl == null ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (result is not null && result.Data is not null)
            {
                Products = result.Data;
            }
            CurrentPage = 1;
            PageCount = 0;

            ProductChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }

        public event Action ProductChanged;

        public async Task SearchProducts(string? searchText, int page)
        {
            LastSearchText = searchText;

            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0) Message = "No products found";

            ProductChanged?.Invoke();

        }
    }
}
