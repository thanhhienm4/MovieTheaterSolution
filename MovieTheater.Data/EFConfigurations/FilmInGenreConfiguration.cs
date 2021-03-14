using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class FilmInGenreConfiguration : IEntityTypeConfiguration<FilmInGenre>
    {
        public void Configure(EntityTypeBuilder<FilmInGenre> builder)
        {
            builder.ToTable("FilmInGenres");
            builder.HasKey(x => new { x.FilmId, x.FilmGenreId });

            builder.HasOne(x => x.Film).WithMany(x => x.FilmInGenres).HasForeignKey(x => x.FilmId);
            builder.HasOne(x => x.FilmGenre).WithMany(x => x.FilmInGenres).HasForeignKey(x => x.FilmGenreId);
        }
    }
}