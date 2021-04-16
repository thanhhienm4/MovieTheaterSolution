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
    public class KindOfScreeningConfiguration : IEntityTypeConfiguration<KindOfScreening>
    {
        public void Configure(EntityTypeBuilder<KindOfScreening> builder)
        {
            builder.ToTable("KindOfScreenings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Surcharge).HasDefaultValue(0);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
