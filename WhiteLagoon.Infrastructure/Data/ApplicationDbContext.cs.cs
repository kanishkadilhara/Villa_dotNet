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
        public DbSet<VillaNumber> VillaNumbers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Villa>().HasData(
             new Villa
               {
                 VillaId = 1,
                 Name = "Royal Villa",
                 Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                 ImageUrl = "https://placehold.co/600x400",
                 occupancy = 4,
                 Price = 200,
                 SquareMeters = 550,
                },
            new Villa
                {
                    VillaId = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    occupancy = 4,
                    Price = 300,
                    SquareMeters = 550,
                },
            new Villa
            {
                VillaId = 3,
                Name = "Luxury Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x402",
                occupancy = 4,
                Price = 400,
                SquareMeters = 750,
            }
           );
           modelBuilder.Entity<VillaNumber>().HasData(
            new VillaNumber
            {
                Villa_Number = 101,
                VillaId = 1,
                SpecialDetails = "This is the special details for villa number 101"
            },
            new VillaNumber
            {
                Villa_Number = 102,
                VillaId = 2,
                SpecialDetails = "This is the special details for villa number 102"
            },
            new VillaNumber
            {
                Villa_Number = 103,
                VillaId = 3,
                SpecialDetails = "This is the special details for villa number 103"
            }
           );
        }

    

    }
}
