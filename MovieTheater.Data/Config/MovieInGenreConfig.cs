using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    internal class MovieInGenreConfig : IEntityTypeConfiguration<MovieInGenre>
    {
        public void Configure(EntityTypeBuilder<MovieInGenre> builder)
        {
            builder.HasKey(e => new { e.GenreId, e.MovieId });

            builder.ToTable("MovieInGenre");

            builder.Property(e => e.GenreId)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.MovieId)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.HasOne(d => d.Genre)
                .WithMany(p => p.MovieInGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Movie)
                .WithMany(p => p.MovieInGenres)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
