using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => new { x.ReservationId, x.SeatId });
            builder.Property(x => x.Price).HasDefaultValue(0);

            builder.HasOne(x => x.Reservation).WithMany(x => x.Tickets).HasForeignKey(x => x.ReservationId);
            builder.HasOne(x => x.Seat).WithMany(x => x.Tickets).HasForeignKey(x => x.SeatId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}