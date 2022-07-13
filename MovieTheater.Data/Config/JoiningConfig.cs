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
    internal class JoiningConfig : IEntityTypeConfiguration<Joining>
    {
        public void Configure(EntityTypeBuilder<Joining> builder)
        {
            builder.HasKey(e => new { e.ActorId, e.MovieId, e.PositionId });

            builder.Property(e => e.MovieId)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.PositionId)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.Actor)
                .WithMany(p => p.Joinings)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Movie)
                .WithMany(p => p.Joinings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Position)
                .WithMany(p => p.Joinings)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
