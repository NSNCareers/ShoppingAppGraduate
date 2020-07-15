using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemFromShoppingCart([FromRoute] int id)
        {
            List<ShoppingCart> results = null;

            results = await _shoppingManager.GetItem(id);

            return Ok(results);
        }
    }
}