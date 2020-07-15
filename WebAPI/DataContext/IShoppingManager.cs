using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DataContext
{
    public interface IShoppingManager
    {
        Task<List<ShoppingCart>> GetItem(int ItemId);
        Task<ShoppingCart> RemoveItem(int itemId);
        Task<List<ShoppingCart>> GetAllItem();
        Task<ShoppingCart> AddItem(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateItem(ShoppingCart shoppingCart);
    }
}
