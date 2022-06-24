﻿// <auto-generated />
using System;
using CapStockRespository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapStockDemo.Migrations
{
    [DbContext(typeof(StockDbContext))]
    [Migration("20210804085125_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CapStockRespository.Models.StockEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProductNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockCount")
                        .HasColumnName("STOCK_COUNT")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnName("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TB_STOCK");

                    b.HasComment("库存表");
                });
#pragma warning restore 612, 618
        }
    }
}
