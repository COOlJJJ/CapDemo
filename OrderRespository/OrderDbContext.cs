using Microsoft.EntityFrameworkCore;
using OrderRespository.Models;
using System;

namespace OrderRespository
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<OrderEntity> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("TB_ORDER");
                entity.HasComment("订单表");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("ORDER_NO")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.ProductName)
                    .HasColumnName("PRODUCT_NAME")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.ProductNo)
                   .HasColumnName("PRODUCT_NO")
                   .HasColumnType("varchar(128)");

                entity.Property(e => e.Count)
                    .HasColumnName("COUNT");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE");
            });
        }
    }
}
