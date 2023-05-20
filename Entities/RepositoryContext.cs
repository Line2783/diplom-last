using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            
            // modelBuilder.ApplyConfiguration(new HotelConfiguration());
            // modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<HotelPhoto> HotelsPhotos { get; set; }





    }
}