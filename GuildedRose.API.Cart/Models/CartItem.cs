﻿using System;
namespace GuildedRose.API.Cart.Models
{
    public class CartItem
    {
        public CartItem()
        {
        }

        public string cartid { get; set; }
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }
}