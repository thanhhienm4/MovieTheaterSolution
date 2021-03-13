using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Data.EFConfigurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Price).HasDefaultValue(0);

            //builder.HasOne(x => x.Reservation).WithMany(x => x.Tickets).HasForeignKey(x => x.ReservationId);
            //builder.HasOne(x => x.Seat).WithMany(x => x.Tickets).HasForeignKey(x => x.SeatId);
        }
    }
}
