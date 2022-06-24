using CapStockRespository.Models;
using Microsoft.EntityFrameworkCore;
using CapStockRespository.Models;
using System;

namespace CapStockRespository
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<StockEntity> Stock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockEntity>(entity =>
            {
                entity.ToTable("TB_STOCK");
                entity.HasComment("库存表");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.StockCount)
                    .HasColumnName("STOCK_COUNT");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE");
            });
        }
    }
}
