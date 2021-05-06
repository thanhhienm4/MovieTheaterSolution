﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Paid).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Time).IsRequired();

            builder.HasOne(x => x.Employee).WithMany(x => x.ReservationsEmployee).HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.Customer).WithMany(x => x.ReservationsCustomer).HasForeignKey(x => x.CustomerId);
           
        }
    }
}