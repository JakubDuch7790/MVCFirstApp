﻿// <auto-generated />
using MVCFirstApp.DataAcces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCFirstApp.DataAcces.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVCFirstApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Alfa"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Beta"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Gama"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Delta"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 5,
                            Name = "Epsilon"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}