using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Data.EFConfigurations
{
    public class SeatRowConfiguration : IEntityTypeConfiguration<SeatRow>
    {
        public void Configure(EntityTypeBuilder<SeatRow> builder)
        {
            builder.ToTable("SeatRows");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
