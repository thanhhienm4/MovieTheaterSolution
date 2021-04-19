using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
           builder.HasIndex(x => x.Name).IsUnique();
            builder.HasOne(x => x.Format).WithMany(x => x.Rooms).HasForeignKey(x => x.FormatId);
        }
    }
}