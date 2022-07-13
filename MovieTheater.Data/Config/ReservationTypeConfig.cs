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
    internal class ReservationTypeConfig : IEntityTypeConfiguration<ReservationType>
    {
        public void Configure(EntityTypeBuilder<ReservationType> builder)
        {
            builder.ToTable("ReservationType");

            builder.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}
