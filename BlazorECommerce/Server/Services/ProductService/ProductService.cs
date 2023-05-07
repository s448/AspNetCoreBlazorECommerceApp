using BlazorECommerce.Server.Data;
using BlazorECommerce.Shared;
using BlazorECommerce.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = dataContext;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();

            var product = new Product();
            /*
            if the user is admin get all the products including the invisible products else then the user is a customer
            so return the visible products only
            */
            if (_contextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products.Include(p => p.ProductVariants.Where(v => !v.Deleted))
                .ThenInclude(v => v.ProductType).Where(p => p.Id == productId && !p.Deleted).FirstOrDefaultAsync();
            }
            else
            {
                product = await _context.Products.Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(v => v.ProductType).Where(p => p.Id == productId && p.Visible && !p.Deleted).FirstOrDefaultAsync();
            }

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
                Data = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) && p.Visible && !p.Deleted).
                Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted)).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted)).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProduct(string searchText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductBySearchText(searchText)).Count / pageResults);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower())
                                    && p.Visible && !p.Deleted)
                                .Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted))
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
                            && p.Visible && !p.Deleted
                            ).Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted)).ToListAsync();
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
                Data = await _context.Products.Where(p => p.Featured && p.Visible && !p.Deleted)
                .Include(p => p.ProductVariants.Where(v => v.Visible && !v.Deleted)).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => !p.Deleted)
                    .Include(p => p.ProductVariants.Where(v => !v.Deleted))
                    .ThenInclude(v => v.ProductType)
                    .ToListAsync()
            };

            return response;
        }


        //Admin's CRUD operations
        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            foreach (var variant in product.ProductVariants)
            {
                variant.ProductType = null;
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products
              .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.Visible = product.Visible;
            dbProduct.Featured = product.Featured;

            foreach (var variant in product.ProductVariants)
            {
                var dbVariant = await _context.productVariants
                    .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
                        v.ProductTypeId == variant.ProductTypeId);
                if (dbVariant == null)
                {
                    variant.ProductType = null;
                    _context.productVariants.Add(variant);
                }
                else
                {
                    dbVariant.ProductTypeId = variant.ProductTypeId;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.Visible = variant.Visible;
                    dbVariant.Deleted = variant.Deleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var dbProduct = await _context.Products.FindAsync(productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
