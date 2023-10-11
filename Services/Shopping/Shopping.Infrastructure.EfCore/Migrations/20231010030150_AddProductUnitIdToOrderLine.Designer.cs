﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopping.Infrastructure.EfCore;

#nullable disable

namespace Shopping.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20231010030150_AddProductUnitIdToOrderLine")]
    partial class AddProductUnitIdToOrderLine
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shopping.Domain.BuyerAggregate.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("Shopping.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderNo"));

                    b.Property<DateTime>("PlacedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Shopping.Domain.OrderAggregate.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UnitName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductUnitId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("Shopping.Domain.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Price")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<Guid>("ProductGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductUnitId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Shopping.Domain.ProductAggregate.ProductReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductReview");
                });

            modelBuilder.Entity("Shopping.Domain.ProductUnitAggregate.ProductUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ProductUnit");
                });

            modelBuilder.Entity("Shopping.Domain.BuyerAggregate.Buyer", b =>
                {
                    b.OwnsOne("Shopping.Domain.BuyerAggregate.ValueObjects.BuyerAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("BuyerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("FullAddress")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("State");

                            b1.HasKey("BuyerId");

                            b1.ToTable("Buyer");

                            b1.WithOwner()
                                .HasForeignKey("BuyerId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Shopping.Domain.OrderAggregate.Order", b =>
                {
                    b.HasOne("Shopping.Domain.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Shopping.Domain.OrderAggregate.OrderContactInfo", "ContactInfo", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("ContactName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ContactName");

                            b1.Property<string>("CustomerName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CustomerName");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Email");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Phone");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("State");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("ContactInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("Shopping.Domain.OrderAggregate.OrderLine", b =>
                {
                    b.HasOne("Shopping.Domain.OrderAggregate.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shopping.Domain.ProductAggregate.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shopping.Domain.ProductUnitAggregate.ProductUnit", null)
                        .WithMany()
                        .HasForeignKey("ProductUnitId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Shopping.Domain.ProductAggregate.Product", b =>
                {
                    b.HasOne("Shopping.Domain.ProductUnitAggregate.ProductUnit", "ProductUnit")
                        .WithMany()
                        .HasForeignKey("ProductUnitId");

                    b.Navigation("ProductUnit");
                });

            modelBuilder.Entity("Shopping.Domain.ProductAggregate.ProductReview", b =>
                {
                    b.HasOne("Shopping.Domain.ProductAggregate.Product", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shopping.Domain.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Shopping.Domain.ProductAggregate.Product", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}