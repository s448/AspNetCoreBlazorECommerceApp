using BlazorECommerce.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
        {
            category.IsNew = category.Editing = false;
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategory(int id)
        {
            Category category = await GetCategoryByID(id);
            if (category == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found"
                };
            }
            category.Deleted = true;
            await _dataContext.SaveChangesAsync();
            return await GetAdminCategories();
        }

        private async Task<Category> GetCategoryByID(int id)
        {
            return await _dataContext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
        {
            var categories = await _dataContext.Categories.Where(c => !c.Deleted).ToListAsync();

            return new ServiceResponse<List<Category>>()
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
            var categories = await _dataContext.Categories.Where(c => c.Visible && !c.Deleted).ToListAsync();

            return new ServiceResponse<List<Category>>()
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
        {
            Category dbCategory = await GetCategoryByID(category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found"
                };
            }

            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Visible = category.Visible;

            await _dataContext.SaveChangesAsync();
            return await GetAdminCategories();
        }
    }
}
