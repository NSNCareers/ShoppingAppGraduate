using Microsoft.AspNetCore.Mvc;
using WebAPI.DataContext;
using WebAPI.ExceptionHandler;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("v2/shopping")]
    [ApiController]
    public class ShoppingCartController2 : ControllerBase
    {
        private readonly IShoppingManager _shoppingManager; 
        public ShoppingCartController2(IShoppingManager shoppingManager)
        {
            _shoppingManager = shoppingManager;
        }

        [HttpGet("{id}")]
        public IActionResult GetItemFromShoppingCart([FromRoute] int id)
        {
            ShoppingCartException results = null;

            results = _shoppingManager.GetItem<ShoppingCartException>(id).Result;

            return Ok(results.ShoppingCarts);
        }
    }
}