using BlazorEcommerce.Shared;
using BlazorECommerce.Server.Services.AuthService;
using BlazorECommerce.Server.Data;
using BlazorECommerce.Server.Services.CartService;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        public OrderService(DataContext context, ICartService cartService, IAuthService authService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId)
        {
            //verify the the response dto carrying data of type OrderDetailsResponse
            var response = new ServiceResponse<OrderDetailsResponse>();

            //get the order using order id param and user id and include the product and product type of each order item
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductType)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            //if the order does't exist prapare the response with false flag and return it
            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }
            //else since the order exist; define the order details response  
            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsProductResponse>()
            };
            //then fill in all the order items into the order details product responses
            order.OrderItems.ForEach(item =>
            orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
            {
                ProductId = item.ProductId,
                ImageUrl = item.Product.ImageUrl,
                ProductType = item.ProductType.Name,
                Quantity = item.Quantity,
                Title = item.Product.Title,
                TotalPrice = item.TotalPrice
            }));
            //carry the order details response in the data 
            response.Data = orderDetailsResponse;
            //return the service response
            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await _context.
                Orders.Include(o => o.OrderItems).
                ThenInclude(oi => oi.Product).Where(o => o.UserId == _authService.GetUserId()).
                OrderByDescending(o => o.OrderDate).
                ToListAsync();

            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                ProductTitle = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Product.Title} and" +
                    $" {o.OrderItems.Count - 1} more..." :
                    o.OrderItems.First().Product.Title,
                ProductImageUrl = o.OrderItems.First().Product.ImageUrl
            }));

            response.Data = orderResponse;

            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            decimal orderTotalPrice = 0;
            products.ForEach(product => orderTotalPrice += product.Price * product.Quantity);

            var orderItems = new List<OrderItem>();

            products.ForEach(product => orderItems.Add(new OrderItem
            {
                ProductId = product.ProductId,
                ProductTypeId = product.ProductTypeId,
                Quantity = product.Quantity,
                TotalPrice = product.Price * product.Quantity,
            }));

            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                OrderItems = orderItems,
                TotalPrice = orderTotalPrice,
            };
            //place the order
            _context.Orders.Add(order);

            //remove your items from the DB
            _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()));

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };

        }
    }
}
