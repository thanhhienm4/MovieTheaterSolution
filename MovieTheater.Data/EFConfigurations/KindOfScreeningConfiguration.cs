using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class KindOfScreeningConfiguration : IEntityTypeConfiguration<KindOfScreening>
    {
        public void Configure(EntityTypeBuilder<KindOfScreening> builder)
        {
            builder.ToTable("KindOfScreenings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Surcharge).HasDefaultValue(0);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}