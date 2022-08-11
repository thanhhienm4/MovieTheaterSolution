using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    internal class SeatRowConfig : IEntityTypeConfiguration<SeatRow>
    {
        public void Configure(EntityTypeBuilder<SeatRow> builder)
        {
            builder.ToTable("SeatRow");
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength(true);
        }
    }
}