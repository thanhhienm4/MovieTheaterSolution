using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;
using MovieTheater.Data.Enums;

namespace MovieTheater.Data.EFConfig
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Dob).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}