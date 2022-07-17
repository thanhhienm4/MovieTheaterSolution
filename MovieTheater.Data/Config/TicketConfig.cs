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
    internal class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");

            builder.Property(e => e.CustomerType)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.HasOne(d => d.CustomerTypeNavigation)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerType)
                .HasConstraintName("FK_Ticket_CustomerType");

            builder.HasOne(d => d.Seat)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK_Ticket_Seat");

            builder.HasOne(d => d.Reservation)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("FK_Ticket_Reservation");
        }
    }
}