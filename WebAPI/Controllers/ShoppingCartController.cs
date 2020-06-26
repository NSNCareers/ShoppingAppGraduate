using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DataContext;
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
        public async Task<IActionResult> RemoveFromShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            var results = await _shoppingManager.RemoveItem(shoppingCart.Id);

            return Ok(results.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemFromShoppingCart()
        {

            var results = await _shoppingManager.GetAllItem();

            return Ok(results);
        }

        [HttpPut]
        public async Task<IActionResult> AddItemToShoppingCart([FromBody]ShoppingCart shoppingCart)
        {
            var results = await _shoppingManager.AddItem(shoppingCart);

            return Ok(results.Message);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShoppingCart([FromBody]ShoppingCart shoppingCart)
        {
            var results = await _shoppingManager.UpdateItem(shoppingCart);

            return Ok(results.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemFromShoppingCart([FromRoute]int id)
        {
            List<ShoppingCart> results;

            results = await _shoppingManager.GetItem(id);
          
            return Ok(results);
        }
    }
}