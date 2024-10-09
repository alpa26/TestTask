using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TestTask.Entities;

namespace TestTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>().ToTable("developers");
            modelBuilder.Entity<Game>().ToTable("games");
            modelBuilder.Entity<Genre>().ToTable("genres");

            modelBuilder.Entity<Game>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Games)
            .UsingEntity(
             "gametogengres",
             r => r.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
             l => l.HasOne(typeof(Game)).WithMany().HasForeignKey("GameId"),
             j => j.HasKey("GameId", "GenreId"));

            modelBuilder.Entity<Game>().HasOne(d => d.Developer).WithMany()
            .HasForeignKey(x => x.DeveloperId).OnDelete(DeleteBehavior.Restrict).IsRequired(true);
        }
    }
}
