using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCFirstApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace MVCFirstApp.DataAcces.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Sedan", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SUV", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Hatchback", DisplayOrder = 3 },
                new Category { Id = 4, Name = "SuperSport", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Hypersport", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Combi", DisplayOrder = 6 }
                );

            modelBuilder.Entity<Company>().HasData(

                new Company { Id = 1, Name = "Tech Solutions", StreetAdress = "PFB1", City = "Presov", Country = "Slovakia", PhoneNumber = "666999696969", PostalCode = "08001" },
                new Company { Id = 2, Name = "Rear Differentials Kingdom", StreetAdress = "PFB1", City = "Presov", Country = "Slovakia", PhoneNumber = "666999696969", PostalCode = "08001" },
                new Company { Id = 3, Name = "White Horse Group", StreetAdress = "PFB1", City = "Presov", Country = "Slovakia", PhoneNumber = "666999696969", PostalCode = "08001" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Brand = "BMW", YearOfConstruction = 2014, Description = "", KilometresDriven = 106524, PowerInKilowatts = 136, Price = 25000, CategoryId= 1, ImageUrl="", CarModel="M3"},
                new Product { Id = 2, Brand = "Mercedes", Description = "", YearOfConstruction = 2016, KilometresDriven = 196524, PowerInKilowatts = 128, Price = 38000, CategoryId = 2, ImageUrl = "", CarModel = "GLE" },
                new Product { Id = 3, Brand = "Seat", Description = "", YearOfConstruction = 2013, KilometresDriven = 326524, PowerInKilowatts = 77, Price = 7500, CategoryId = 3, ImageUrl = "", CarModel = "Ibiza" },
                new Product { Id = 4, Brand = "Skoda", Description = "", YearOfConstruction = 2012, KilometresDriven = 126524, PowerInKilowatts = 84, Price = 3600, CategoryId = 3, ImageUrl = "", CarModel = "Felicia" },
                new Product { Id = 5, Brand = "Suzuki", Description = "", YearOfConstruction = 2004, KilometresDriven = 136524, PowerInKilowatts = 55, Price = 500, CategoryId = 3, ImageUrl = "", CarModel = "Swift" },
                new Product { Id = 6, Brand = "Citroen", Description = "", YearOfConstruction = 2010, KilometresDriven = 116524, PowerInKilowatts = 103, Price = 2999, CategoryId = 3, ImageUrl = "", CarModel = "C5" }
                );
            modelBuilder.Entity<Product>()
                .Property(b => b.IsAvailable)
                .HasDefaultValue(true);
            //modelBuilder.Entity<ShoppingCart>().Property(x => x.Id)
            //        .HasColumnName("Id")
            //        .HasColumnType("int")
            //        .ValueGeneratedOnAdd();
            //* *.UseIdentityColumn(); **
            //modelBuilder.Entity<ShoppingCart>().Property(p => p.Id).HasValueGenerator
        }
    }
}
