using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
    {
        public void Configure(EntityTypeBuilder<Screening> builder)
        {
            builder.ToTable("Screenings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
           // builder.Property(x => x.Surcharge).HasDefaultValue(0);
            builder.Property(x => x.TimeStart).IsRequired();

            builder.HasOne(x => x.Film).WithMany(x => x.Screenings).HasForeignKey(x => x.FilmId);
            builder.HasOne(x => x.Room).WithMany(x => x.Screenings).HasForeignKey(x => x.RoomId);
        }
    }
}