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
    public class CartRepository : ICartRepository
    {
        private readonly CartContext _context;

        public CartRepository(CartContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<CartItem>> AddItem(string id, CartItem cartitem)
        {
            bool existingitem = false;

            var cartModel =  await _context.Cart.Where(c => c.Id == id).Include(p => p.Items).SingleOrDefaultAsync();

            if (cartModel == null)
            {
                CartModel newcartmodel = new CartModel()
                {
                    Id = id,
                    Items = new List<CartItem>()
                };
                newcartmodel.Items.Add(new CartItem()
                {
                    cartid = id,
                    Id = cartitem.Id,
                    Price = cartitem.Price,
                    Quantity = cartitem.Quantity
                });

                _context.Cart.Add(newcartmodel);
            }
            else
            {
                //They've tried to add the same cart item.
                //Don't add it again, just increase the quantity
                foreach (CartItem item in cartModel.Items)
                {
                    if (item.Id == cartitem.Id)
                    {
                        item.Quantity += cartitem.Quantity;
                        existingitem = true;
                        break;
                    };
                };

                //This item does not exist in the cart
                if (!existingitem) cartModel.Items.Add(cartitem);
            }

             await _context.SaveChangesAsync();

            return cartitem;
        }

        public async Task<ActionResult<CartModel>> GetCart(string id)
        {
            return await _context.Cart.Where(c => c.Id == id).Include(p => p.Items).SingleOrDefaultAsync();
        }

        //public void CreateCart(CartModel cart)
        //{
        //    _context.Cart.Add(cart);
        //}

        //public async Task<int> Save()
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}
