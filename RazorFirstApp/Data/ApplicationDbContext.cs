using Microsoft.EntityFrameworkCore;
using RazorFirstApp.Models;

namespace RazorFirstApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Alfa", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "Beta", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "Gama", DisplayOrder = 3 }
                        );
        }

    }
}
