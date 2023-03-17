﻿using BlazorECommerce.Shared.DTO;

namespace BlazorECommerce.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponse>>> GetCartItems(List<CardItem> cartItems);
        Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CardItem> cardItems, int userId);
    }
}
