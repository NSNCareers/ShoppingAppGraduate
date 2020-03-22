using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Model;

namespace WebAPI.Data
{
    internal class ShopingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(x => x.CustomerName)
               .IsRequired()
               .HasMaxLength(20);
            builder.Property(x => x.Gender)
              .IsRequired();
            builder.Property(x => x.Price)
              .IsRequired()
              .HasColumnType("decimal(5,2)");
            builder.Property(x => x.OrderQuantity)
            .IsRequired();
        }
    }
}
