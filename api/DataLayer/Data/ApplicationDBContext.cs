using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Portfolio>().HasKey(p => new { p.AppUserId, p.Stockid});
            builder.Entity<Portfolio>().HasOne(p => p.AppUser).WithMany(p => p.Portfolios).HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>().HasOne(p => p.Stock).WithMany(p => p.Portfolios).HasForeignKey(p => p.Stockid);
            List<IdentityRole> roles =
            [
                new IdentityRole{Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{Name = "User", NormalizedName = "USER"}
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}