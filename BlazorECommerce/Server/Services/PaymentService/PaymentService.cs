using BlazorECommerce.Server.Services.AuthService;
using BlazorECommerce.Server.Services.OrderService;
using BlazorECommerce.Server.Services.CartService;
using Stripe.Checkout;
using Stripe;

namespace BlazorECommerce.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IAuthService _authService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        const string secret = "whsec_3b47d567446a83b0d99149c9f84abc074077e8d0178338d289ae9a1300ee1d27";
        public PaymentService(IAuthService authService, ICartService cartService, IOrderService orderService)
        {
            StripeConfiguration.ApiKey = "sk_test_51N1zBRL9Xr7OqbdgD1EEXggTKddXbMiSFurXr7kxwtFEa773qZSeN7iaKiZgUSvMxKYFACQR05fA4b8KTIPEFR82009vZC7Oai";
            _authService = authService;
            _cartService = cartService;
            _orderService = orderService;
        }
        public async Task<Session> CreateCheckoutSerssion()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();

            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImgUrl }
                    }
                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = { "US", "EG" }
                },

                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7230/order-success",
                CancelUrl = "https://localhost:7230/cart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        secret
                    );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }

                return new ServiceResponse<bool> { Data = true };
            }
            catch (StripeException e)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }
    }
}
