﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    internal class SurchargeConfig : IEntityTypeConfiguration<Surcharge>
    {
        public void Configure(EntityTypeBuilder<Surcharge> builder)
        {
            builder.ToTable("Surcharge");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.AuditoriumFormatId)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.Property(e => e.Price).HasColumnType("money");

            builder.Property(e => e.SeatType)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.HasOne(d => d.AuditoriumFormat)
                .WithMany(p => p.Surcharges)
                .HasForeignKey(d => d.AuditoriumFormatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.SeatTypeNavigation)
                .WithMany(p => p.Surcharges)
                .HasForeignKey(d => d.SeatType)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}