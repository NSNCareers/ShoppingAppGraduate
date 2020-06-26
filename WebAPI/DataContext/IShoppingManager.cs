using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.ExceptionHandler;
using WebAPI.Model;

namespace WebAPI.DataContext
{
    public interface IShoppingManager
    {
        Task<List<ShoppingCart>> GetItem(int ItemId);
        Task<ShoppingCartException> RemoveItem(int itemId);
        Task<List<ShoppingCart>> GetAllItem();
        Task<ShoppingCartException> AddItem(ShoppingCart shoppingCart);
        Task<ShoppingCartException> UpdateItem(ShoppingCart shoppingCart);
    }
}
