﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Context;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    partial class ShoppingCartContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Model.CustomerAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartId");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("PostCode");

                    b.Property<string>("Street");

                    b.Property<string>("Town");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new { Id = 1, CartId = 1, HouseNumber = 22, PostCode = "CV2 3HH", Street = "London Street", Town = "Coventry" }
                    );
                });

            modelBuilder.Entity("WebAPI.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartId");

                    b.Property<string>("Seller");

                    b.Property<int>("Size");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Items");

                    b.HasData(
                        new { Id = 1, CartId = 1, Seller = "Amazon", Size = 34, Weight = 1002 }
                    );
                });

            modelBuilder.Entity("WebAPI.Model.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("OrderQuantity")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");

                    b.HasData(
                        new { Id = 1, CustomerName = "Jonathan", Gender = "Male", OrderQuantity = "900 KG", Price = 34m }
                    );
                });

            modelBuilder.Entity("WebAPI.Model.CustomerAddress", b =>
                {
                    b.HasOne("WebAPI.Model.ShoppingCart", "Cart")
                        .WithOne("Address")
                        .HasForeignKey("WebAPI.Model.CustomerAddress", "CartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Model.Item", b =>
                {
                    b.HasOne("WebAPI.Model.ShoppingCart", "Cart")
                        .WithMany("Item")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
