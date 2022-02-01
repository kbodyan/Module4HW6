using Microsoft.EntityFrameworkCore;

namespace MusicEF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<ArtistSong> ArtistSong { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Genre> Genre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistSongConfiguration());
            modelBuilder.ApplyConfiguration(new SongConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder
            .Entity<Artist>()
            .HasMany(c => c.Songs)
            .WithMany(s => s.Artists)
            .UsingEntity<ArtistSong>(r => r.HasOne(r => r.Song).WithMany(r => r.ArtistSong).HasForeignKey(r => r.SongId), l => l.HasOne(l => l.Artist).WithMany(l => l.ArtistSong).HasForeignKey(l => l.ArtistID));
        }
    }
}