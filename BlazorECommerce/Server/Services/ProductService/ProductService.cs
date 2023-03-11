using BlazorECommerce.Server.Data;
using BlazorECommerce.Shared;
using BlazorECommerce.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.Include(p => p.ProductVariants)
                .ThenInclude(v => v.ProductType).Where(p => p.Id == productId).FirstOrDefaultAsync();

            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry this product doesn't exist";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).
                Include(p => p.ProductVariants).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Include(p => p.ProductVariants).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProduct(string searchText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductBySearchText(searchText)).Count / pageResults);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()))
                                .Include(p => p.ProductVariants)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private Task<List<Product>> FindProductBySearchText(string searchText)
        {
            return _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower())
                            ).Include(p => p.ProductVariants).ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetSearchSuggestions(string searchText)
        {
            var products = await FindProductBySearchText(searchText);
            List<string> results = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.ToLower().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(product.Title);
                }
                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !results.Contains(word))
                        {
                            results.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>>()
            {
                Data = results
            };
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Where(p => p.Featured)
                .Include(p => p.ProductVariants).ToListAsync()
            };
            return response;
        }

    }
}
