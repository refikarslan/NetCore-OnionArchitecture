using Microsoft.EntityFrameworkCore;
using NetCore_OnionArchitecture.Application.Interfaces;
using NetCore_OnionArchitecture.Domain.Common;
using NetCore_OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }    
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Added:
                        break;
                    case EntityState.Modified:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                // Tablo adı
                entity.ToTable("Customers");

                // Anahtar
                entity.HasKey(c => c.Id);

          
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.DateOfBirth)
                    .IsRequired();

                entity.Property(c => c.Tel)
                    .HasMaxLength(11); // Telefon numarası uzunluğunu belirleyin
             
            });

        }
    }
}

