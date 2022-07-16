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
    internal class StaffConfig : IEntityTypeConfiguration<staff>
    {
        public void Configure(EntityTypeBuilder<staff> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(e => e.UserName);

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
                .HasMaxLength(128);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(d => d.RoleNavigation)
                .WithMany(p => p.staff)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Role");
        }
    }
}
