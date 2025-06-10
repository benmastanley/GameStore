using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Context
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Game entity
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Price).HasColumnType("decimal(18,2)");
                entity.Property(g => g.ReleaseDate).HasColumnType("date");
                
                // Configure the relationship with Genre
                entity.HasOne(g => g.Genre)
                    .WithMany(g => g.Games)
                    .HasForeignKey(g => g.GenreId);
            });
            // Configure the Genre entity
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(50);
            });
        }
    }
}
