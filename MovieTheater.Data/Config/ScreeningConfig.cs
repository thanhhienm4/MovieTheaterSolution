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
    public class ScreeningConfig : IEntityTypeConfiguration<Screening>
    {
        public void Configure(EntityTypeBuilder<Screening> builder)
        {
            builder.ToTable("Screening");

            builder.Property(e => e.AuditoriumId)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.MovieId)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.HasOne(d => d.Auditorium)
                .WithMany(p => p.Screenings)
                .HasForeignKey(d => d.AuditoriumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Screening_Auditorium");

            builder.HasOne(d => d.Movie)
                .WithMany(p => p.Screenings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Screening_Movies");
        }
    }
}
