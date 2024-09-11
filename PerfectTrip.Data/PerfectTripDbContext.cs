using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Domain.Entities.Data;
using PerfectTrip.Domain.Entities.Orders;
using PerfectTrip.Domain.Entities.Products;
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

            modelBuilder.Entity<Facility>()
                .HasIndex(f => f.FacilityName)
                .IsUnique();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductDetailFacility> ProductDetailFacilities { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderResident> OrderResidents { get; set; }
    }
}
