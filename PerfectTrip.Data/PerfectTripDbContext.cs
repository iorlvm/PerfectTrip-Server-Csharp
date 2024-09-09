using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PerfectTrip.Common.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data
{
    public class PerfectTripDbContext : DbContext
    {

        public PerfectTripDbContext(DbContextOptions<PerfectTripDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Admin> Admins  { get; set; }
    }
}
