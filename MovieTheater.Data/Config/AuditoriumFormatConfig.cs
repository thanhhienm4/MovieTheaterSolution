using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    public class AuditoriumFormatConfig : IEntityTypeConfiguration<AuditoriumFormat>
    {
        public void Configure(EntityTypeBuilder<AuditoriumFormat> builder)
        {
            builder.ToTable("AuditoriumFormat");

            builder.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}