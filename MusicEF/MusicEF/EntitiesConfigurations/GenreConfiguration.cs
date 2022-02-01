using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicEF
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre").HasKey(e => e.Id);
            builder.Property(p => p.Id).HasColumnName("GenreId").ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired();
        }
    }
}