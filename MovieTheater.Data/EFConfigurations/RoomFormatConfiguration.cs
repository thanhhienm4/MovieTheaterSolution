using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class RoomFormatConfiguration : IEntityTypeConfiguration<RoomFormat>
    {
        public void Configure(EntityTypeBuilder<RoomFormat> builder)
        {
            builder.ToTable("RoomFormats");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}