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
    internal class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation");

            builder.Property(e => e.Customer)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeId)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.TypeId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            //builder.Property(e => e.VoucherId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            builder.HasOne(d => d.CustomerNavigation)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Customer);

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.EmployeeId);

            builder.HasOne(d => d.PaymentStatusNavigation)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PaymentStatus)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Screening)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ScreeningId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(d => d.Type)
                .WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(d => d.Voucher)
            //    .WithMany(p => p.Reservations)
            //    .HasForeignKey(d => d.VoucherId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}