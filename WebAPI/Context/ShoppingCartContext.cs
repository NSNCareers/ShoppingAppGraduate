using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Model;

namespace WebAPI.Context
{
    public class ShoppingCartContext : DbContext
    {
        //This entity will be translated into data base tables
        //Virtual to prevent eager loading
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<CustomerAddress> Addresses { get; set; }

        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configures our DB set
            modelBuilder.Entity<ShoppingCart>();
            modelBuilder.Entity<Item>()
                .HasOne(a => a.Cart)
                .WithMany(b => b.Item);
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(a => a.Cart)
                .WithOne(b => b.Address);
            base.OnModelCreating(modelBuilder);
            //Adds data to DB set
            modelBuilder.ApplyConfiguration(new ShopingCartConfig());
            modelBuilder.SeedShoppingCartData();
            modelBuilder.SeedItemData();
            modelBuilder.SeedAddressData();
        }
    }
}
