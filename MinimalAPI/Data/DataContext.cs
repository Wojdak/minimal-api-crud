using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, Name = "Max Verstappen", Nationality = "Dutch", RacingNumber = 1, Team = "Red Bull Racing" },
                new Driver { Id = 2, Name = "Sergio Perez", Nationality = "Mexican", RacingNumber = 11, Team = "Red Bull Racing" },
                new Driver { Id = 3, Name = "Lewis Hamilton", Nationality = "British", RacingNumber = 44, Team = "Mercedes" },
                new Driver { Id = 4, Name = "Carlos Sainz", Nationality = "Spanish", RacingNumber = 55, Team = "Ferrari" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin_account", Password = "Test123!", Email = "admin@test.eu", Role = "Administrator" },
                new User { Id = 2, Username = "standard_account", Password = "Test123!", Email = "standard@test.eu", Role = "Standard" }
            );
        }
    }
}
