using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    internal class TicketPriceConfig : IEntityTypeConfiguration<TicketPrice>
    {
        public void Configure(EntityTypeBuilder<TicketPrice> builder)
        {
            builder.ToTable("TicketPrice");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.AuditoriumFormat)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CustomerType)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.FromTime).HasColumnType("datetime");

            builder.Property(e => e.Price).HasColumnType("money");

            builder.Property(e => e.TimeId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ToTime).HasColumnType("datetime");

            builder.HasOne(d => d.AuditoriumFormatNavigation)
                .WithMany(p => p.TicketPrices)
                .HasForeignKey(d => d.AuditoriumFormat);

            builder.HasOne(d => d.CustomerTypeNavigation)
                .WithMany(p => p.TicketPrices)
                .HasForeignKey(d => d.CustomerType)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Time)
                .WithMany(p => p.TicketPrices)
                .HasForeignKey(d => d.TimeId);
        }
    }
}