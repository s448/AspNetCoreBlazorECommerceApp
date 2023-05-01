using BlazorEcommerce.Shared;
using BlazorECommerce.Server.Services.AddressService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Address>>> GetAddress()
        {
            var address = await _addressService.GetAddress();
            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Address>>> AddOrUpdateAddress(Address address)
        {
            var response = await _addressService.AddOrUpdateAddress(address);
            return Ok(response);
        }
    }
}
