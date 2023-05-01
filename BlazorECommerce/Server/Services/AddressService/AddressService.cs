using BlazorEcommerce.Shared;
using BlazorECommerce.Server.Data;
using BlazorECommerce.Server.Services.AuthService;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;
        public AddressService(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }
        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            var response = new ServiceResponse<Address>();

            //if there is no address we define a one with user id of the current user id and save it to DB
            //if the address exist so the CRUD operation is update tho we will override all the address variables then save the changes
            //the last thing is to assign the new address to the service response and return it
            var dbAddress = (await GetAddress()).Data;
            if (dbAddress == null)
            {
                address.UserId = _authService.GetUserId();
                _context.Addresses.Add(address);
                response.Data = address;
            }
            else
            {
                dbAddress.FirstName = address.FirstName;
                dbAddress.LastName = address.LastName;
                dbAddress.State = address.State;
                dbAddress.Country = address.Country;
                dbAddress.City = address.City;
                dbAddress.Zip = address.Zip;
                dbAddress.Street = address.Street;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authService.GetUserId();
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
            return new ServiceResponse<Address> { Data = address };
        }
    }
}
