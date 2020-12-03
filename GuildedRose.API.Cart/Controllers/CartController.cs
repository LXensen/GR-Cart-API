using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuildedRose.API.Cart.Models;
using GuildedRose.API.Cart.Interfaces;
using System.Web.Http.Cors;

using GuildedRose.API.Cart.Services;

namespace GuildedRose.API.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _service;

        public CartController(ICartRepository service)
        {
            _service = service;
        }

        // GET: api/Cart/CartId
        [HttpGet("{id}")]
        public async Task<ActionResult<CartModel>> GetCart(string id)
        {
            var cartModel = await _service.GetCart(id);

            if (cartModel == null)
            {
                return NotFound();
            }

            return cartModel;
        }

        //// POST: api/Cart
        [HttpPost("{id}")]
        public async Task<ActionResult<CartItem>> AddItem(string id, CartItem cartitem)
        {
            await _service.AddItem(id, cartitem);
            return CreatedAtAction(nameof(AddItem), new { id = cartitem.Id }, cartitem);
        }

        //// DELETE: api/Cart/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCartModel(string id)
        //{
        //    var cartModel = await _context.Cart.FindAsync(id);
        //    if (cartModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cart.Remove(cartModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
