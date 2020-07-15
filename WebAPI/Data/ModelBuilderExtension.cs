using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Data
{
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        //Shopping Cart Data 
        public static void SeedShoppingCartData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>().HasData
                (
                new ShoppingCart
                {
                    CustomerName = "Jonathan",
                    Gender = "Male",
                    Id = 1,
                    OrderQuantity = "900 KG",
                    Price = 34m,
                    Message = ""
                }
                );         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        //Item data
        public static void SeedItemData(this ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Item>().HasData
               (
                  new Item
                  {
                   Id = 1,
                   CartId=1,
                   Size = 34,
                   Weight = 1002,
                   Seller = "Amazon",
                  }
               );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedAddressData(this ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<CustomerAddress>().HasData
               (
                new CustomerAddress
                {
                    Id= 1,
                    CartId=1,
                    HouseNumber = 22,
                    Street = "London Street",
                    Town = "Coventry",
                    PostCode = "CV2 3HH"
                }
               );
        }
    }
}
