using Microsoft.EntityFrameworkCore;
using ToyStore.Domain.Entities;

namespace ToyStore.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Toy> Toys { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Toy>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Type).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Price).IsRequired();

                entity.HasOne(s => s.Store).WithMany(t => t.Toys)
                .HasForeignKey(s => s.StoreId).IsRequired();
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            });
        }
    }
}
