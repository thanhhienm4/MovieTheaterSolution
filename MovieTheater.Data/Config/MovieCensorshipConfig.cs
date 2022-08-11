using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    public class MovieCensorshipConfig : IEntityTypeConfiguration<MovieCensorship>
    {
        public void Configure(EntityTypeBuilder<MovieCensorship> builder)
        {
            builder.ToTable("MovieCensorship");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}