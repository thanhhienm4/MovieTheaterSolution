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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.KindOfSeat).WithMany(x => x.Seats).HasForeignKey(x => x.KindOfSeatId);
            builder.HasOne(x => x.Room).WithMany(x => x.Seats).HasForeignKey(x => x.RoomId);
        }
    }
}
