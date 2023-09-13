﻿// <auto-generated />
using System;
using Customers.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Customers.Infrastructure.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20230913044738_InitData")]
    partial class InitData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Customers.Domain.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a783af07-5704-4e93-8a5d-2983783f47b3"),
                            CustomerName = "Dennis Homenick"
                        },
                        new
                        {
                            Id = new Guid("a527f245-694c-4893-982d-becaf29f0013"),
                            CustomerName = "Litzy Cummerata"
                        },
                        new
                        {
                            Id = new Guid("4b0eb601-c0f5-423e-88f9-f16fe893ce0b"),
                            CustomerName = "Norma Maggio"
                        },
                        new
                        {
                            Id = new Guid("6c79c789-0212-48d6-aa4d-87e339e2090d"),
                            CustomerName = "Shanna Labadie"
                        },
                        new
                        {
                            Id = new Guid("9953bfba-b380-48df-aa0a-33c28eb7ee74"),
                            CustomerName = "Hosea Turner"
                        },
                        new
                        {
                            Id = new Guid("79c8e7f8-56e4-4b91-a969-6a9e37b22882"),
                            CustomerName = "Sidney Hessel"
                        },
                        new
                        {
                            Id = new Guid("456c2a0d-1e23-4ee6-81ce-16b814786501"),
                            CustomerName = "Isidro VonRueden"
                        },
                        new
                        {
                            Id = new Guid("13bd7333-247f-4563-95e7-78e54a24aabf"),
                            CustomerName = "Isac Torp"
                        },
                        new
                        {
                            Id = new Guid("497c2b02-042c-4667-b232-dcc55216bb6a"),
                            CustomerName = "Hillary Beer"
                        },
                        new
                        {
                            Id = new Guid("0a3d88e0-f6b2-47cd-aaa0-bbc1292f8939"),
                            CustomerName = "Avis Gislason"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
