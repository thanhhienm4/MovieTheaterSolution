using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    internal class PaymentStatusConfig : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.ToTable("PaymentStatus");

            builder.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);
        }
    }
}