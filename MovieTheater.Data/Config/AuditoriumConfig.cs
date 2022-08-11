using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    public class AuditoriumConfig : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.ToTable("Auditorium");

            builder.Property(e => e.Id)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.FormatId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name).HasMaxLength(64);

            builder.HasOne(d => d.Format)
                .WithMany(p => p.Auditoriums)
                .HasForeignKey(d => d.FormatId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}