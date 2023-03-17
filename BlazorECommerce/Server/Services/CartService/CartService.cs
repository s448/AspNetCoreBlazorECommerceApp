using BlazorECommerce.Server.Data;
using BlazorECommerce.Shared;
using BlazorECommerce.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorECommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartItems(List<CardItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var productVariant = await _context.productVariants
                    .Where(v => v.ProductId == item.ProductId
                        && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImgUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CardItem> cardItems,int userId)
        {
            cardItems.ForEach(ci => ci.UserId =  userId);
            _context.CartItems.AddRange(cardItems);
            await _context.SaveChangesAsync();

            return await GetCartItems(await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync());
        }
    }
}
