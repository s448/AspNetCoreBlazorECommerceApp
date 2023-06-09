﻿namespace BlazorECommerce.Client.Services.ProductServices
{
    public interface IProductServices
    {
        event Action ProductChanged;
        List<Product> Products { set; get; }
        List<Product> AdminProducts { set; get; }
        Task GetProducts(string? categoryUrl = null);
        Task GetAdminProducts();
        Task<ServiceResponse<Product>> GetProductById(int productId);
        //start => pagination params
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        //end => pagination params
        string Message { set; get; }
        Task SearchProducts(string? searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
