namespace BlazorECommerce.Client.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        List<ProductType> ProductTypes { get; set; }
        event Action OnChange;
        Task GetProductTypes();
        Task AddProductType(ProductType productType);
        Task UpdateProductType(ProductType productType);
        ProductType CreateNewProductType();

    }
}
