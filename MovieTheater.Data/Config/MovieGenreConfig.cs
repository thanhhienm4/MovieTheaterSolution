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
    public class MovieGenreConfig : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
