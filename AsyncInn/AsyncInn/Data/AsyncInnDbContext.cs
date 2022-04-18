using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        //to include a new table into your database
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Amenity>().HasData(
              new Amenity { Id = 1, Name = "coffee maker" },
              new Amenity { Id = 2, Name = "ocean view" },
              new Amenity { Id = 3, Name = "air conditioning" }

            );
            modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Sheraton", StreetAddress= "15205 North Kierland Blvd. Suite 100", City= "Anchorage", State= "Alaska", Country="US", Phone="123" },
            new Hotel { Id = 2, Name = "Fairmont", StreetAddress = "854 Avocado Ave.", City = "Los Angeles", State = "California", Country = "US", Phone = "456" },
            new Hotel { Id = 3, Name = "Le Royal", StreetAddress = "298 Beachwalk ", City = "Jacksonville[f]", State = "Florida", Country = "US", Phone = "789" }

          );
            modelBuilder.Entity<Room>().HasData(
             new Room { Id = 1, Name = "Studio",Layout = 0 },
             new Room { Id = 2, Name = "One Bedroom", Layout = 1 },
             new Room { Id = 3, Name = "Two Bedroom", Layout = 2 }

           );
        }
    }
}
