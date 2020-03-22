using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DataContext;
using WebAPI.ExceptionHandler;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("v1/shopping")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingManager _shoppingManager; 
        public ShoppingCartController(IShoppingManager shoppingManager)
        {
            _shoppingManager = shoppingManager;
        }

        [HttpDelete]
        public IActionResult RemoveFromShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            var results =_shoppingManager.RemoveItem<ShoppingCartException>(shoppingCart.Id).Result;

            return Ok(results.Message);
        }

        [HttpGet]
        public IActionResult GetAllItemFromShoppingCart()
        {

            var results = _shoppingManager.GetAllItem<object>().Result;

            return Ok(results);
        }

        [HttpPut]
        public IActionResult AddItemToShoppingCart([FromBody]ShoppingCart shoppingCart)
        {
            var results = _shoppingManager.AddItem<ShoppingCartException>(shoppingCart).Result;

            return Ok(results.Message);
        }

        [HttpPost]
        public IActionResult UpdateShoppingCart([FromBody]ShoppingCart shoppingCart)
        {
            var results = _shoppingManager.UpdateItem<ShoppingCartException>(shoppingCart).Result;

            return Ok(results.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemFromShoppingCart([FromRoute]int id)
        {
            ShoppingCartException results;

            results = _shoppingManager.GetItem<ShoppingCartException>(id).Result;
          
            return Ok(results.ShoppingCarts);
        }
    }
}