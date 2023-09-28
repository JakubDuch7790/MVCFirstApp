using Microsoft.EntityFrameworkCore;
using MVCFirstApp.Models;

namespace MVCFirstApp.DataAcces.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Alfa", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Beta", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Gama", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Delta", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Epsilon", DisplayOrder = 5 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Brand = "BMW", YearOfConstruction = 2014, KilometresDriven = 106524, PowerInKilowatts = 136, Price = 25000 },
                new Product { Id = 2, Brand = "Mercedes", YearOfConstruction = 2016, KilometresDriven = 196524, PowerInKilowatts = 128, Price = 38000 },
                new Product { Id = 3, Brand = "Seat", YearOfConstruction = 2013, KilometresDriven = 326524, PowerInKilowatts = 77, Price = 7500 },
                new Product { Id = 4, Brand = "Skoda", YearOfConstruction = 2012, KilometresDriven = 126524, PowerInKilowatts = 84, Price = 3600 },
                new Product { Id = 5, Brand = "Suzuki", YearOfConstruction = 2004, KilometresDriven = 136524, PowerInKilowatts = 55, Price = 500 },
                new Product { Id = 6, Brand = "Citroen", YearOfConstruction = 2010, KilometresDriven = 116524, PowerInKilowatts = 103, Price = 2999 }
                );
        }
    }
}
