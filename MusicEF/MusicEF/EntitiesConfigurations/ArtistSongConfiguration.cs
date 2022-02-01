using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicEF
{
    public class ArtistSongConfiguration : IEntityTypeConfiguration<ArtistSong>
    {
        public void Configure(EntityTypeBuilder<ArtistSong> builder)
        {
            builder.ToTable("ArtistSong").HasKey(e => new { e.ArtistID, e.SongId });
            builder.Property(p => p.ArtistID).IsRequired();
            builder.HasOne(p => p.Artist).WithMany(e => e.ArtistSong).HasForeignKey(p => p.ArtistID).OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.SongId).IsRequired();
            builder.HasOne(p => p.Song).WithMany(e => e.ArtistSong).HasForeignKey(p => p.SongId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}