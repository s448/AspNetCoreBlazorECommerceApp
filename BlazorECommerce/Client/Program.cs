global using System.Net.Http.Json;
global using BlazorECommerce.Shared;
global using Microsoft.AspNetCore.Components.Authorization;
global using BlazorECommerce.Client.Services.ProductTypeService;
using BlazorECommerce.Client;
using BlazorECommerce.Client.Services.ProductServices;
using BlazorECommerce.Client.Services.CategoryService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using BlazorECommerce.Client.Services.CardService;
using BlazorECommerce.Client.Services.AuthService;
using BlazorECommerce.Client.Services.OrderService;
using BlazorECommerce.Client.Services.AddressService;
using BlazorEcommerce.Client.Services.AddressService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
