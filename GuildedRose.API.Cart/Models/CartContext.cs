using Microsoft.EntityFrameworkCore;
using GuildedRose.API.Cart.Models;

namespace GuildedRose.API.Cart.Models
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }

        public DbSet<CartModel> Cart { get; set; }
    }
}
