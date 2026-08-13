using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain;
namespace WhiteLagoon.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Villa>().HasData(
             new Villa
               {
                 Id = 1,
                 Name = "Royal Villa",
                 Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                 ImageUrl = "https://placehold.co/600x400",
                 occupancy = 4,
                 Price = 200,
                 SquareMeters = 550,
                },
            new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    occupancy = 4,
                    Price = 300,
                    SquareMeters = 550,
                },
            new Villa
            {
                Id = 3,
                Name = "Luxury Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x402",
                occupancy = 4,
                Price = 400,
                SquareMeters = 750,
            }
           );
        }

    

    }
}
