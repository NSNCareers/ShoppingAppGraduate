using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.DAL;
using WebAPI.Model;

namespace WebAPI.DataContext
{
    public class ShoppingManager : IShoppingManager
    {
        private readonly IDataBaseChanges _dataBaseChanges;
        private readonly ShoppingCartContext _context;
        private readonly ILogger<ShoppingManager> _logger;

        public ShoppingManager(ShoppingCartContext context,IDataBaseChanges dataBaseChanges, ILogger<ShoppingManager> logger)
        {
            _dataBaseChanges = dataBaseChanges;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public async Task<List<ShoppingCart>> GetItem(int ItemId)
        {
            var shoppingCart = new List<ShoppingCart>();

            try
            {
                    shoppingCart = await _context.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Address)
                    .Where( s => s.Id == ItemId)
                    .ToListAsync();

                var results = shoppingCart.GetEnumerator();
                int id = ItemId;

                while (!results.MoveNext())
                {
                    _logger.LogInformation($"User with ID :{ItemId} does not exist");

                    return new List<ShoppingCart> { new ShoppingCart { Message = $"User with ID:{ItemId} does not exist" } };
                }

                _logger.LogInformation($"Successfully returned User with ID :{ItemId}");
                return shoppingCart;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
               
            }

            return new List<ShoppingCart>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShoppingCart>> GetAllItem() 
        {
            try
            {
                var allItems = await _context.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Address)
                    .ToListAsync();

                if (allItems.Count == 0)
                {
                    _logger.LogInformation($"No user exist in shopping cart");

                    return new List<ShoppingCart> { new ShoppingCart { Message = $"No user exist in shopping cart" } };
                }

                return allItems;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting all users from DB => {ex.InnerException}");
            }

            _logger.LogInformation("Successfully returned all users from shopping cart");

            return new List<ShoppingCart>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public async  Task<ShoppingCart> AddItem(ShoppingCart shoppingCart)
        {
            try
            {
                await _dataBaseChanges.AddAsync(shoppingCart);
                await _dataBaseChanges.CommitAsync();
                _logger.LogInformation("Successfully committed changes in Database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during adding item to DB => {ex.InnerException}");

            }

            return new ShoppingCart
            {
                Message = $"Successfully added Item with Id:{shoppingCart.Id}"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> RemoveItem(int itemId)
        {
            try
            {
                var results = await _context.ShoppingCarts
                    .FindAsync(itemId);
                if (results == null)
                {
                    return new ShoppingCart
                    {
                        Message = $"User with Id: {itemId} does not exist in shopping cart"
                    } ;
                }

                _dataBaseChanges.Remove(results);
                await _dataBaseChanges.CommitAsync();
                _logger.LogInformation("Successfully removed and committed changes in Database");

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
            }

            return new ShoppingCart
            {
                Message = $"Successfully removed User with Id:{itemId}"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> UpdateItem(ShoppingCart shoppingCart)
        {

            try
            {
                var results = await _context.ShoppingCarts.FindAsync(shoppingCart.Id);

                if (results == null)
                {
                    return new ShoppingCart
                    {
                        Message = $"User with Id: {shoppingCart.Id} does not exist in shopping cart"
                    } ;
                }
                //To solve the problem of tracking entity. Make sure entity is in stable state
                _context.Entry(results).State = EntityState.Detached;
                _dataBaseChanges.Update(shoppingCart);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.InnerException}");
            }

            return new ShoppingCart
            {
                Message=$"Successfully updated user with Id:{shoppingCart.Id}"
            };
        }
    }
}
