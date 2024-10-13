﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy_2.Data;

#nullable disable

namespace Pharmacy_2.Migrations
{
    [DbContext(typeof(PharmacyContext))]
    [Migration("20231120154609_User-change")]
    partial class Userchange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pharmacy.Classes.InventoryProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<long>("ProductUPC")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "UserId", "ProductUPC");

                    b.HasIndex("ProductUPC");

                    b.HasIndex("UserId");

                    b.ToTable("InventoryProducts");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            UserId = 1,
                            ProductUPC = 1234567890L,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("Pharmacy.Classes.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Pharmacy.Classes.Products.Product", b =>
                {
                    b.Property<long>("UPC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UPC"));

                    b.Property<long>("EDRPOU")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UPC");

                    b.ToTable("Products", t =>
                        {
                            t.HasCheckConstraint("EDRPOU", "LEN(EDRPOU) = 8");
                        });

                    b.HasDiscriminator<string>("ProductType").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Pharmacy.Classes.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "Яна",
                            Surname = "Алексюк"
                        },
                        new
                        {
                            UserId = 2,
                            Name = "Євгеній",
                            Surname = "Жидик"
                        },
                        new
                        {
                            UserId = 5,
                            Name = "Ясос",
                            Surname = "Біба"
                        });
                });

            modelBuilder.Entity("Pharmacy.Classes.Products.Consumables", b =>
                {
                    b.HasBaseType("Pharmacy.Classes.Products.Product");

                    b.Property<string>("ConsumableType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501));

                    b.ToTable("Products", t =>
                        {
                            t.HasCheckConstraint("EDRPOU", "LEN(EDRPOU) = 8");

                            t.Property("ExpirationDate")
                                .HasColumnName("Consumables_ExpirationDate");
                        });

                    b.HasDiscriminator().HasValue("Consumables");

                    b.HasData(
                        new
                        {
                            UPC = 1234567890L,
                            EDRPOU = 88888888L,
                            Name = "Mediprop",
                            Price = 250.5m,
                            ConsumableType = "Syringe",
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Pharmacy.Classes.Products.Devices", b =>
                {
                    b.HasBaseType("Pharmacy.Classes.Products.Product");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable(t =>
                        {
                            t.HasCheckConstraint("EDRPOU", "LEN(EDRPOU) = 8");
                        });

                    b.HasDiscriminator().HasValue("Devices");

                    b.HasData(
                        new
                        {
                            UPC = 986235712L,
                            EDRPOU = 11111111L,
                            Name = "Інгалятор",
                            Price = 850.25m,
                            DeviceType = "Inhaler"
                        });
                });

            modelBuilder.Entity("Pharmacy.Classes.Products.Drugs", b =>
                {
                    b.HasBaseType("Pharmacy.Classes.Products.Product");

                    b.Property<string>("DrugType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 11, 20, 17, 46, 8, 926, DateTimeKind.Local).AddTicks(575));

                    b.Property<bool>("NeedRecipe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.ToTable(t =>
                        {
                            t.HasCheckConstraint("EDRPOU", "LEN(EDRPOU) = 8");
                        });

                    b.HasDiscriminator().HasValue("Drugs");
                });

            modelBuilder.Entity("Pharmacy.Classes.InventoryProduct", b =>
                {
                    b.HasOne("Pharmacy.Classes.Products.Product", "Product")
                        .WithMany("InventoryProducts")
                        .HasForeignKey("ProductUPC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Classes.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Classes.Order", "Order")
                        .WithMany("InventoryProducts")
                        .HasForeignKey("OrderId", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pharmacy.Classes.Order", b =>
                {
                    b.HasOne("Pharmacy.Classes.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pharmacy.Classes.Order", b =>
                {
                    b.Navigation("InventoryProducts");
                });

            modelBuilder.Entity("Pharmacy.Classes.Products.Product", b =>
                {
                    b.Navigation("InventoryProducts");
                });

            modelBuilder.Entity("Pharmacy.Classes.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
