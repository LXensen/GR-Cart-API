using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GuildedRose.API.Cart.Models;
using GuildedRose.API.Cart.Interfaces;
using GuildedRose.API.Cart.Controllers;
using Moq;

namespace GuildedRose.API.Cart.Tests.Unit
{
    public class CartUnitTest
    {
        string cartid = "123445677";

        [Fact]
        public async Task AddItemToCart_success()
        {
            var mockRepo = new Mock<ICartService>();
            var cartController = new CartController(mockRepo.Object);
            cartController.ModelState.AddModelError("error", "base error");

            CartItem cartitem = new CartItem()
            {
                Id = "A11111",
                cartid = cartid,
                Price = "10.00",
                Quantity = 1
            };

            var result = await cartController.AddItem(cartid, cartitem);

            Assert.Equal(cartitem, (result.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public async Task AddItemToCart_NoIdPassed_Fail()
        {
            CartItem cartitem = new CartItem()
            {
                Id = "A11111",
                cartid = cartid,
                Price = "10.00",
                Quantity = 1
            };

            var mockRepo = new Mock<ICartService>();
            mockRepo.Setup(repo => repo.AddItem(cartid, cartitem)).Returns(Task.CompletedTask);
            var cartController = new CartController(mockRepo.Object);
            cartController.ModelState.AddModelError("error", "base error");



            var result = await cartController.AddItem("", cartitem);

            Assert.Equal(cartitem, (result.Result as CreatedAtActionResult).Value);
        }
    }
}
