// File: Data/ApplicationDbContext.cs
using DotnetBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: extra configuration
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");
                entity.HasKey(s => s.StaffId);
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.Email).IsRequired();
                entity.Property(s => s.Username).IsRequired();
                entity.Property(s => s.PasswordHash).IsRequired();
                entity.Property(s => s.PrimaryNumber).IsRequired();
                entity.Property(s => s.Role).IsRequired();
                entity.HasIndex(s => s.Username).IsUnique();
                entity.HasIndex(s => s.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
