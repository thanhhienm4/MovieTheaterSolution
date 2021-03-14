using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Paid).HasDefaultValue(false);
            builder.Property(x => x.Active).HasDefaultValue(true);

            builder.HasOne(x => x.Employee).WithMany(x => x.ReservationsEmployee).HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.User).WithMany(x => x.ReservationsUser).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Screening).WithMany(x => x.Reservations).HasForeignKey(x => x.ScreeningId);
        }
    }
}