using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuildedRose.API.Cart.Models;

namespace GuildedRose.API.Cart.Interfaces
{
    public interface ICartService
    {
        Task<ActionResult<CartModel>> GetCart(string id);
        
        Task<ActionResult<CartModel>> AddItem(string id, CartItem cartitem);

        void CreateCart(CartModel cart);

        Task<int> Save();
    }
}
