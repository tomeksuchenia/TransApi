using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;

namespace Trans.Infrascture.EF_DB
{
    public class TransContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
   
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public TransContext(DbContextOptions<TransContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().OwnsOne(c => c.Adress);
            modelBuilder.Entity<Company>().HasMany(c => c.Vehicles).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().HasMany(c => c.Drivers).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().HasMany(c => c.Orders).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().OwnsOne(o => o.OrderCompany, oc =>
            {
                oc.OwnsOne(a => a.Adress);
            });
            modelBuilder.Entity<Order>().OwnsOne(o => o.Load);

            modelBuilder.Entity<Vehicle>().Property(v => v.VehicleId).ValueGeneratedNever();
            modelBuilder.Entity<Driver>().Property(d => d.DriverId).ValueGeneratedNever();

            modelBuilder.Entity<User>(u =>
            {
                u.Property(u => u.Password).HasMaxLength(150);
                u.Property(u => u.Username).HasMaxLength(50);
            });

        }
    }
}
