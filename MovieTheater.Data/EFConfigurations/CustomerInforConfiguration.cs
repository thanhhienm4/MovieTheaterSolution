using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;
using MovieTheater.Data.Enums;

namespace MovieTheater.Data.EFConfig
{
    public class CustomerInforConfiguration : IEntityTypeConfiguration<CustomerInfor>
    {
        public void Configure(EntityTypeBuilder<CustomerInfor> builder)
        {
            builder.ToTable("CustomerInfors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Dob).IsRequired();
            builder.HasOne(x => x.User).WithOne(x => x.CustomerInfor).HasForeignKey<CustomerInfor>(x => x.Id);


        }
    }
}