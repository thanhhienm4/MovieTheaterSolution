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
    internal class VoucherConfig : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");

            builder.Property(e => e.Id)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.Property(e => e.MaxValue).HasColumnType("money");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Operator)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength(true);

            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.Property(e => e.Value).HasColumnType("money");
        }
    }
}