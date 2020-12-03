using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GuildedRose.API.Cart.Models;
using GuildedRose.API.Cart.Interfaces;
using GuildedRose.API.Cart.Services;
using Microsoft.EntityFrameworkCore;

namespace GuildedRose.API.Cart.Tests.Unit
{
    public class CartUnitTest
    {
        string cartid = "123445677";

        [Fact]
        public void AddItemToCart_success()
        {
            CartItem cartitem = new CartItem()
            {
                Id = "B11111",
                cartid = cartid,
                Price = "20.00",
                Quantity = 1
            };

            var options = new DbContextOptionsBuilder<CartContext>().UseInMemoryDatabase(databaseName: "CartItems").Options;


            using (var context = new CartContext(options))
            {
                CartRepository _controller = new CartRepository(context);
                var ok = _controller.AddItem(cartid, cartitem);

                Assert.IsAssignableFrom<CartItem>(ok.Result.Value);
            }
        }


        [Fact]
        public void Get_Cart_success()
        {
            CartItem cartitem = new CartItem()
            {
                Id = "A11111",
                cartid = cartid,
                Price = "10.00",
                Quantity = 1
            };

            var options = new DbContextOptionsBuilder<CartContext>().UseInMemoryDatabase(databaseName: "CartItems").Options;


            using (var context = new CartContext(options))
            {
                CartRepository _controller = new CartRepository(context);
                var ok = _controller.AddItem(cartid, cartitem);

                Assert.IsAssignableFrom<CartItem>(ok.Result.Value);

                var cart = _controller.GetCart(cartid);

                Assert.IsAssignableFrom<CartModel>(cart.Result.Value);
            }
        }

        [Fact]
        public void Get_NonExistingCart_ShouldBeNull()
        {
            var invalidCartId = "abcdef";

            var options = new DbContextOptionsBuilder<CartContext>().UseInMemoryDatabase(databaseName: "CartItems").Options;


            using (var context = new CartContext(options))
            {
                CartRepository _controller = new CartRepository(context);
   

                var cart = _controller.GetCart(invalidCartId);

                Assert.Null(cart.Result.Value);
            }
        }
    }
}
