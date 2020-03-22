using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DataContext
{
    public interface IShoppingManager
    {
        Task<T> GetItem<T>(int ItemId) where T : class;
        Task<T> RemoveItem<T>(int itemId) where T : class;
        Task<List<T>> GetAllItem<T>() where T : class;
        Task<T> AddItem<T>(ShoppingCart shoppingCart) where T : class;
        Task<T> UpdateItem<T>(ShoppingCart shoppingCart) where T : class;
    }
}
