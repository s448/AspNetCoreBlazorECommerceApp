namespace BlazorECommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        List<Category> Categories { set; get; }
        Task GetCategories();
    }
}
