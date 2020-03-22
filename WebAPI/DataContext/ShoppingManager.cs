using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public ShoppingManager(ShoppingCartContext context,IDataBaseChanges dataBaseChanges)
        {
            _dataBaseChanges = dataBaseChanges;
            _context = context;
        }

        public async Task<T> GetItem<T>(int ItemId) where T : class
        {
            ShoppingCart shoppingCart;
            try
            {
                shoppingCart = await _context.ShoppingCarts
                    .FindAsync(ItemId);

                if (shoppingCart == null)
                {
                    return new ShoppingCartException
                    {
                        Message = $"Item with Id: {ItemId} does not exist in shopping cart"
                    } as T;
                }
            }
            catch (Exception ex)
            {
                return new ShoppingCartException
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                } as T;
               
            }

            return new ShoppingCartException
            {
               ShoppingCarts=shoppingCart
            } as T;
        }

        public async Task<List<T>> GetAllItem<T>() where T : class
        {
            var shop = new List<object>();
            try
            {
                  var  results = await _context.ShoppingCarts
                    .Join
                    (
                    _context.Items,
                    cartId => cartId.Id,
                    itemId => itemId.Id,
                    (cartId, itemId) => new
                    {
                        ID=cartId.Id,
                        Gender=cartId.Gender,
                        CustomerName= cartId.CustomerName,
                        Price= cartId.Price,
                        OrderQuantity= cartId.OrderQuantity,
                        Size= itemId.Size,
                        Weight= itemId.Weight,
                        Seller= itemId.Seller
                    }
                    )
                    .Join
                    (
                    _context.Addresses,
                     cartId => cartId.ID,
                    AddresssId => AddresssId.Id,
                    (cartId, AddresssId) => new
                    {
                        ID= cartId.ID,
                        Gender= cartId.Gender,
                        CustomerName= cartId.CustomerName,
                        Price= cartId.Price,
                        OrderQuantity= cartId.OrderQuantity,
                        Street= AddresssId.Street,
                        HouseNumber= AddresssId.HouseNumber,
                        Postcode= AddresssId.PostCode,
                        Town= AddresssId.Town
                    }
                    )
                    .ToListAsync();

                if (results == null)
                {
                    return new ShoppingCartException
                    {
                        Message = "Shopping cart does not contain any items"
                    } as List<T>;
                }

                foreach (var item in results)
                {
                    shop.Add(item);
                }

                return shop as List<T>;
            }
            catch (Exception ex)
            {
                return new ShoppingCartException
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                } as List<T>;
            }
        }

        public async  Task<T> AddItem<T>(ShoppingCart shoppingCart) where T : class
        {
            try
            {
                await _dataBaseChanges.AddAsync(shoppingCart);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {

                return new ShoppingCartException
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                } as T;
            }

            return new ShoppingCartException
            {
                BoolResults = true,
                Message = $"Successfully added Item with Id:{shoppingCart.Id}"
            } as T;
        }

        public async Task<T> RemoveItem<T>(int itemId) where T : class
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
                    } as T;
                }
                _dataBaseChanges.Remove(results);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                return new ShoppingCartException
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                } as T;
            }

            return new ShoppingCartException
            {
                BoolResults = true,
                Message = $"Successfully removed Item with Id:{itemId}"
            } as T;
        }

        public async Task<T> UpdateItem<T>(ShoppingCart shoppingCart) where T : class
        {

            try
            {
                var results = await _context.ShoppingCarts.FindAsync(shoppingCart.Id);
                if (results == null)
                {
                    return new ShoppingCartException
                    {
                        Message = $"Item with Id: {shoppingCart.Id} does not exist in shopping cart"
                    } as T;
                }
                //To solve the problem of tracking entity. Make sure entity is in stable state
                _context.Entry(results).State = EntityState.Detached;
                _dataBaseChanges.Update(shoppingCart);
                await _dataBaseChanges.CommitAsync();
            }
            catch (Exception ex)
            {
                return new ShoppingCartException
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException
                } as T;
            }

            return new ShoppingCartException
            {
                Message=$"Successfully updated Item with Id:{shoppingCart.Id}"
            } as T;
        }
    }
}
