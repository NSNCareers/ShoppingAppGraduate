using System.Collections.Generic;
using WebAPI.Model;

namespace WebAPI.Data
{
    public class ShoppingCartData : IShoppingCartData
    {
        public ShoppingCart InitShoppingCart()
        {
            var shoppingCart = new ShoppingCart
            {
                CustomerName = "Jonathan",
                Gender="Male",
                OrderQuantity = "800 KG",
                Price = 34m,
                Address = new CustomerAddress
                {
                    HouseNumber = 22,
                    Street = "London Street",
                    Town = "Coventry",
                    PostCode = "CV2 3HH"
                },
                Item = new List<Item>
                {
                   new Item
                   {
                    Size=34,
                    Weight=1002,
                    Seller="Amazon"
                   },
                    new Item
                   {
                    Size=35,
                    Weight=1003,
                    Seller="Argos"
                   },
                },
              
            };

            return shoppingCart;
        }
    }
}
