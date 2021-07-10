using HotelListing.Configurations.Entities;
using HotelListing.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DataBaseContext : IdentityDbContext<ApiUser>
    {
        public DataBaseContext(DbContextOptions options)
            :base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<Country> Countries { get; set; }
        
        public DbSet<Hotel> Hotels { get; set; }
    }
}