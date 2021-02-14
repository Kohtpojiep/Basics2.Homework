﻿// <auto-generated />
using System;
using Basics2.Homework.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Basics2.Homework.DataAccess.MSSQL.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20210212172137_CreateDB")]
    partial class CreateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Basics2.Homework.DataAccess.MSSQL.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(65)
                        .HasColumnType("nvarchar(65)")
                        .HasColumnName("name");

                    b.Property<short>("Volume")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Basics2.Homework.DataAccess.MSSQL.Entities.Showcase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date")
                        .HasColumnName("create_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(65)
                        .HasColumnType("nvarchar(65)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("date")
                        .HasColumnName("remove_date");

                    b.Property<short>("Volume")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("Showcases");
                });

            modelBuilder.Entity("Basics2.Homework.DataAccess.MSSQL.Entities.ShowcaseProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("ProductCost")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)")
                        .HasColumnName("product_cost");

                    b.Property<short>("ProductCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("product_count");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("ShowcaseId")
                        .HasColumnType("int")
                        .HasColumnName("showcase_id");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShowcaseId");

                    b.ToTable("Showcase_Product");
                });

            modelBuilder.Entity("Basics2.Homework.DataAccess.MSSQL.Entities.ShowcaseProduct", b =>
                {
                    b.HasOne("Basics2.Homework.DataAccess.MSSQL.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Basics2.Homework.DataAccess.MSSQL.Entities.Showcase", "Showcase")
                        .WithMany()
                        .HasForeignKey("ShowcaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Showcase");
                });
#pragma warning restore 612, 618
        }
    }
}
