using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicEF
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Song").HasKey(e => e.Id);
            builder.Property(p => p.Id).HasColumnName("SongId").ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Duration).IsRequired();
            builder.Property(p => p.ReleasedDate).IsRequired();
            builder.HasOne(p => p.Genre).WithMany(e => e.Songs).HasForeignKey(p => p.GenreId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}