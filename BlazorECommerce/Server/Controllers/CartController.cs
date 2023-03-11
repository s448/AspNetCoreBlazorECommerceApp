using BlazorECommerce.Server.Services.CartService;
using BlazorECommerce.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProducts(List<CardItem> cartItems)
        {
            var result = await _cartService.GetCartItems(cartItems);
            return Ok(result);
        }

    }
}
