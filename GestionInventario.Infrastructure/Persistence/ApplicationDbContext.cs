using GestionInventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GestionInventario.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================
            // Configuración de User
            // ========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                // Value Object Username
                entity.OwnsOne(u => u.Username, username =>
                {
                    username.Property(p => p.Value)
                        .HasColumnName("Username")
                        .HasMaxLength(50)
                        .IsRequired();
                });

                // Value Object Email
                entity.OwnsOne(u => u.Email, email =>
                {
                    email.Property(p => p.Value)
                        .HasColumnName("Email")
                        .HasMaxLength(100)
                        .IsRequired();
                });

                // Value Object PasswordHash
                entity.OwnsOne(u => u.PasswordHash, ph =>
                {
                    ph.Property(p => p.Value)
                        .HasColumnName("PasswordHash")
                        .HasMaxLength(200)
                        .IsRequired();
                });

                entity.Property(u => u.Role)
                    .HasConversion<string>() // Guarda el enum como texto
                    .IsRequired();
            });

            // ========================
            // Configuración de Product
            // ========================
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                // Value Object Price
                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(p => p.Value)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                });

                // Value Object StockQuantity
                entity.OwnsOne(p => p.Quantity, q =>
                {
                    q.Property(p => p.Value)
                        .HasColumnName("Quantity")
                        .IsRequired();
                });

                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ========================
            // Configuración de Category
            // ========================
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // ========================
            // Configuración de Notification
            // ========================
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Message)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.HasOne(n => n.Product)
                    .WithMany()
                    .HasForeignKey(n => n.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
