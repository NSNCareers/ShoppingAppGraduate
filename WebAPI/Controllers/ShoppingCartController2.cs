using Microsoft.AspNetCore.Mvc;
using WebAPI.DataContext;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("v2/shopping")]
    [ApiController]
    public class ShoppingCartController2 : ControllerBase
    {
        private readonly IShoppingManager _shoppingManager;
        private readonly IUserTokenGenerator _userTokenGenerator;

        public ShoppingCartController2(IShoppingManager shoppingManager, IUserTokenGenerator userTokenGenerator)
        {
            _shoppingManager = shoppingManager;
            _userTokenGenerator = userTokenGenerator;
        }

        [HttpGet("{userId}")]
        public IActionResult GetToken([FromRoute]int userId)
        {
            var results = _userTokenGenerator.GenerateToken(userId);

            return Ok(results);
        }
    }
}