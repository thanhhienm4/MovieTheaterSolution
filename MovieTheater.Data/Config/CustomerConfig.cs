using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;
using MovieTheater.Data.Models;

namespace MovieTheater.Data.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.UserName);

            builder.ToTable("Customer");

            builder.Property(e => e.UserName)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Mail)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(16);
        }
    }
}
