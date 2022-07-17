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
    internal class TimeConfig : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Time");

            builder.Property(e => e.TimeId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.DateEnd)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.DateStart)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(2550)
                .IsUnicode(false);
        }
    }
}