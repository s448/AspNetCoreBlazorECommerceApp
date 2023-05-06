using BlazorECommerce.Server.Data;
using BlazorECommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext _dataContext;
        public ProductTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType)
        {
            productType.IsNew = productType.Editing = false;
            _dataContext.ProductTypes.Add(productType);
            await _dataContext.SaveChangesAsync();
            return await GetProductTypes();
        }

        public async Task<ServiceResponse<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _dataContext.ProductTypes.ToListAsync();
            return new ServiceResponse<List<ProductType>> { Data = productTypes };
        }

        public async Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType)
        {
            var dbProductType = await _dataContext.ProductTypes.FindAsync(productType);
            if (dbProductType == null)
            {
                return new ServiceResponse<List<ProductType>>
                {
                    Success = false,
                    Message = "product type not found"
                };

                dbProductType.Name = productType.Name;
                await _dataContext.SaveChangesAsync();

            }
            return await GetProductTypes();

        }
    }
}
