using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuildedRose.API.Cart.Models;

namespace GuildedRose.API.Cart.Interfaces
{
    public interface ICartRepository
    {
        Task<ActionResult<CartModel>> GetCart(string id);

        Task<ActionResult<CartItem>> AddItem(string id, CartItem cartitem);
    }
}
