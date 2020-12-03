using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildedRose.API.Cart.Interfaces;
using GuildedRose.API.Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuildedRose.API.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly CartContext _context;

        public CartService(CartContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<CartModel>> AddItem(string id, CartItem cartitem)
        {
            return await _context.Cart.Where(c => c.Id == id).Include(p => p.Items).SingleOrDefaultAsync();
            //var cartModel = _context.Cart.Where(c => c.Id == id).Include(p => p.Items).SingleOrDefault();

            //if (cartModel == null)
            //{
            //    CartModel newcartmodel = new CartModel()
            //    {
            //        Id = id,
            //        Items = new List<CartItem>()
            //    };
            //    newcartmodel.Items.Add(new CartItem()
            //    {
            //        cartid = id,
            //        Id = cartitem.Id,
            //        Price = cartitem.Price,
            //        Quantity = cartitem.Quantity
            //    });
            //    _context.Cart.Add(newcartmodel);
            //}
            //else
            //{
            //    if (cartModel.Items.Contains(cartitem))
            //    {
            //        // increment the inventory coutn
            //    }
            //    else
            //    {
            //        cartModel.Items.Add(cartitem);
            //    }

            //}

            // return _context.SaveChangesAsync();
        }

        public async Task<ActionResult<CartModel>> GetCart(string id)
        {
            return await _context.Cart.Where(c => c.Id == id).Include(p => p.Items).SingleOrDefaultAsync();
        }

        public void CreateCart(CartModel cart)
        {
            _context.Cart.Add(cart);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
