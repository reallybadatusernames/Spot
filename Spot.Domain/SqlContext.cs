using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Spot.Domain.Entities;

namespace Spot.Domain
{
    public class SqlContext : IdentityDbContext
    {
        public SqlContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles and users
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "861fbbcd-4bfd-4ab2-b12c-fe75a7fd53d7", Name = "Administrator", NormalizedName = "Administrator".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "518acc2c-cfd8-4713-ac27-ecdbac4291fd", Name = "User", NormalizedName = "User".ToUpper() });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "917d367a-2f4f-45cc-8d1c-e4e01ab42c7f",
                Email = "admin@Spot.com",
                FirstName = "admin",
                LastName = "admin",
                PasswordHash = hasher.HashPassword(null, "$p0tDis")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "861fbbcd-4bfd-4ab2-b12c-fe75a7fd53d7",
                UserId = "917d367a-2f4f-45cc-8d1c-e4e01ab42c7f"
            });

            builder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// A Site to be monitored.
        /// </summary>
        public DbSet<Site> Sites { get; set; }
        
        /// <summary>
        /// A job to be performed against a site.
        /// </summary>
        public DbSet<Fetch> Fetches { get; set; }

        /// <summary>
        /// The result of a job that has ran.
        /// </summary>
        public DbSet<FetchResult> FetchResults { get; set; }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            //put any initial data here.
        }
    }
}
