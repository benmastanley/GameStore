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
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "The Legend of Zelda: Breath of the Wild", Price = 59.99m, ReleaseDate = new DateOnly(2017, 3, 3), GenreId = 1 },
                new Game { Id = 2, Name = "Super Mario Odyssey", Price = 59.99m, ReleaseDate = new DateOnly(2017, 10, 27), GenreId = 1 },
                new Game { Id = 3, Name = "Minecraft", Price = 26.95m, ReleaseDate = new DateOnly(2011, 11, 18), GenreId = 2 }
            );
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "Role-Playing" },
                new Genre { Id = 4, Name = "Simulation" }
            );

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
