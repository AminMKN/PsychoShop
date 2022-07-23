﻿using PsychoShop.Application.Contracts.ShopCart;

namespace PsychoShop.Application
{
    public class CartService : ICartService
    {
        public Cart Cart { get; set; }

        public void Set(Cart cart)
        {
            Cart = cart;
        }

        public Cart Get()
        {
            return Cart;
        }
    }
}
