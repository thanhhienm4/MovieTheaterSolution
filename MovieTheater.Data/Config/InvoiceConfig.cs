using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");
            builder.HasKey(x => x.Id);
            builder
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            builder
                .Property(e => e.PaymentId)
                .IsFixedLength()
                .IsUnicode(false);

            builder.HasOne(x => x.Reservation)
                .WithOne(x => x.Invoice)
                .HasForeignKey<Invoice>(e => e.ReservationId);

            builder.HasOne(x => x.Payment)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.PaymentId);
        }
    }
}