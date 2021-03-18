using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Row).HasColumnType("varchar(1)");
            builder.HasAlternateKey(x => new { x.Row, x.Number, x.RoomId });
            builder.HasOne(x => x.KindOfSeat).WithMany(x => x.Seats).HasForeignKey(x => x.KindOfSeatId);
            builder.HasOne(x => x.Room).WithMany(x => x.Seats).HasForeignKey(x => x.RoomId);
        }
    }
}