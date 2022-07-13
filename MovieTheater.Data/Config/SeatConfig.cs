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
    internal class SeatConfig : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seat");

            builder.HasIndex(e => new { e.RowId, e.Number, e.AuditoriumId })
                .IsUnique();

            builder.HasIndex(e => e.Id)
                .IsUnique();

            builder.Property(e => e.AuditoriumId)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.TypeId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.Auditorium)
                .WithMany(p => p.Seats)
                .HasForeignKey(d => d.AuditoriumId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Row)
                .WithMany(p => p.Seats)
                .HasForeignKey(d => d.RowId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Type)
                .WithMany(p => p.Seats)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
