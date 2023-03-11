namespace BlazorECommerce.Client.Services.ProductServices
{
    public interface IProductServices
    {
        event Action ProductChanged;
        List<Product> Products { set; get; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProductById(int productId);
        //start => pagination params
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        //end => pagination params
        string Message { set; get; }
        Task SearchProducts(string? searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
