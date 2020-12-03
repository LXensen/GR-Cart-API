using System;
using System.Collections.Generic;

namespace GuildedRose.API.Cart.Models
{
    public class CartModel
    {
        public CartModel()
        {
        }

        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
