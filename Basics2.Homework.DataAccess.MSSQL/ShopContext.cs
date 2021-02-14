using System;
using Basics2.Homework.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basics2.Homework.DataAccess.MSSQL
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Showcase> Showcases { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShowcaseProduct> ShowcaseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Showcase>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65)
                    .HasColumnName("name");

                entity.Property(e => e.Volume)
                    .IsRequired()
                    .HasDefaultValue(1)
                    .HasColumnType("smallint")
                    .HasColumnName("volume");

                entity.Property(e => e.CreateDate)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("create_date");

                entity.Property(e => e.RemoveDate)
                    .HasColumnType("date")
                    .HasColumnName("remove_date");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65)
                    .HasColumnName("name");

                entity.Property(e => e.Volume)
                    .IsRequired()
                    .HasDefaultValue(1)
                    .HasColumnType("smallint")
                    .HasColumnName("volume");
            });

            modelBuilder.Entity<ShowcaseProduct>(entity =>
            {
                entity.ToTable("Showcase_Product");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ShowcaseId)
                    .IsRequired()
                    .HasColumnName("showcase_id");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductCount)
                    .IsRequired()
                    .HasDefaultValue(1)
                    .HasColumnType("smallint")
                    .HasColumnName("product_count");

                entity.Property(e => e.ProductCost)
                    .IsRequired()
                    .HasPrecision(12,2)
                    .HasColumnName("product_cost");
            });
        }
    }
}
