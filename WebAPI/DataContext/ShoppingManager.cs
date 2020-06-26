using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.DAL;
using WebAPI.ExceptionHandler;
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
        }

        public async Task<List<ShoppingCart>> GetItem(int ItemId)
        {
            try
            {
                var shoppingCart = await _context.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Address)
                    .Where( s => s.Id == ItemId)
                    .ToListAsync();


                return shoppingCart;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
               
            }

            return new List<ShoppingCart>();
        }


        public async Task<List<ShoppingCart>> GetAllItem() 
        {
            try
            {
                return await _context.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Address)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
            }

            return new List<ShoppingCart>();
        }

        public async  Task<ShoppingCartException> AddItem(ShoppingCart shoppingCart)
        {
            try
            {
                await _dataBaseChanges.AddAsync(shoppingCart);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
            }

            return new ShoppingCartException
            {
                BoolResults = true,
                Message = $"Successfully added Item with Id:{shoppingCart.Id}"
            };
        }

        public async Task<ShoppingCartException> RemoveItem(int itemId)
        {
            try
            {
                var results = await _context.ShoppingCarts
                    .FindAsync(itemId);
                if (results == null)
                {
                    return new ShoppingCartException
                    {
                        Message = $"Item with Id: {itemId} does not exist in shopping cart"
                    } ;
                }
                _dataBaseChanges.Remove(results);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
            }

            return new ShoppingCartException
            {
                BoolResults = true,
                Message = $"Successfully removed Item with Id:{itemId}"
            };
        }

        public async Task<ShoppingCartException> UpdateItem(ShoppingCart shoppingCart)
        {

            try
            {
                var results = await _context.ShoppingCarts.FindAsync(shoppingCart.Id);
                if (results == null)
                {
                    return new ShoppingCartException
                    {
                        Message = $"Item with Id: {shoppingCart.Id} does not exist in shopping cart"
                    } ;
                }
                //To solve the problem of tracking entity. Make sure entity is in stable state
                _context.Entry(results).State = EntityState.Detached;
                _dataBaseChanges.Update(shoppingCart);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during getting item from DB => {ex.Message}");
            }

            return new ShoppingCartException
            {
                Message=$"Successfully updated Item with Id:{shoppingCart.Id}"
            };
        }
    }
}
