using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfig
{
    public class UserInforConfiguration : IEntityTypeConfiguration<UserInfor>
    {
        public void Configure(EntityTypeBuilder<UserInfor> builder)
        {
            builder.ToTable("UserInfors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Dob).IsRequired();
            builder.HasOne(x => x.User).WithOne(x => x.UserInfor).HasForeignKey<UserInfor>(x => x.Id);
        }
    }
}