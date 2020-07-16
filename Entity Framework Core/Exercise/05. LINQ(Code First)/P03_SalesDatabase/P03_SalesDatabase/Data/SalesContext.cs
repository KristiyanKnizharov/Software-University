using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }
        public SalesContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(
                    @"Server=DESKTOP-DPV1UGJ; Database=HospitalDatabase;Integrated Security= true");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsUnicode(true);
                entity.Property(p => p.Description)
                    .HasDefaultValue("No description");
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsUnicode(true);
                entity.Property(c => c.Email)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(s => s.Name)
                    .IsUnicode(true);
            });
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(s => s.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                entity.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);

                entity.HasOne(s => s.Store)
                    .WithMany(st => st.Sales)
                    .HasForeignKey(s => s.StoreId);

                entity.Property(p => p.Date)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
