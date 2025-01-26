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

        #region DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

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

                // Id 
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

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Addresses");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Detail)
                .HasMaxLength(500)
                .IsUnicode(false);  // IsUnicode(false) ayarı, veritabanı tarafında bu sütunun VARCHAR veri türü olarak oluşturulmasını sağlar. Eğer IsUnicode(true) veya IsUnicode() kullanılmazsa, varsayılan olarak NVARCHAR veri türü kullanılır. VarChar'da sadece ingilizce vce batı dilindekieler kullanılır. Nvarchar ise her tür dilde kullanılır.

                entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.PostaCode)
                .HasMaxLength(10)
                .IsUnicode(false);

                entity.Property(e => e.State)
               .HasMaxLength(50)
               .IsUnicode(false);

                entity.Property(e => e.IsDefault)
               .HasMaxLength(500);

                entity.HasOne(e => e.User)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Carts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.TotalPrice)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.HasOne(e => e.User)
                .WithMany(e => e.Carts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.UserId)
                .IsRequired();

                entity.HasMany(e => e.CartItems)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItems");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Quantity)
                .IsRequired();

                entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

                entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

                entity.Property(e => e.ProductId)
                .IsRequired();

                entity.Property(e => e.CartId)
                .IsRequired();

                entity.HasOne(e => e.Cart)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsRequired();

                entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()") // Varsayılan değer olarak o anki tarihi alır
                .IsRequired();

                entity.Property(o => o.UserId)
                .IsRequired();

                entity.Property(o => o.PaymentId)
                .IsRequired();

                entity.HasOne(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Payment)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OrderItems)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItems");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Payment>(entity =>
            {

                entity.ToTable("Payments");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.PaymentDate)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.Payment)
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.Stock)
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.CartItems)
                    .WithOne(e => e.Product)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OrderItems)
                    .WithOne(e => e.Product)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.EMail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.CreatedDate)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(u => u.Carts)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Addresses)
                    .WithOne(a => a.User)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}


