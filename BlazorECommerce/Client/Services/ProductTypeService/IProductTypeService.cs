namespace BlazorECommerce.Client.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        List<ProductType> ProductTypes { get; set; }
        event Action OnChange;
        Task GetProductTypes();

    }
}
