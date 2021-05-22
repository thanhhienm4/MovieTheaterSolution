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
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowId);
            builder.HasKey(x => new { x.RowId, x.Number, x.RoomId });
            builder.HasOne(x => x.KindOfSeat).WithMany(x => x.Seats).HasForeignKey(x => x.KindOfSeatId);
            builder.HasOne(x => x.SeatRow).WithMany(x => x.Seats).HasForeignKey(x => x.RowId);
            builder.HasOne(x => x.Room).WithMany(x => x.Seats).HasForeignKey(x => x.RoomId);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}