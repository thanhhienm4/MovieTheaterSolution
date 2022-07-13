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
    internal class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(e => e.Id)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.CensorshipId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PublishDate).HasColumnType("date");

            builder.Property(e => e.TrailerUrl).HasColumnName("TrailerURL");

            builder.HasOne(d => d.Censorship)
                .WithMany(p => p.Movies)
                .HasForeignKey(d => d.CensorshipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movies_MovieCensorship");
        }
    }
}
