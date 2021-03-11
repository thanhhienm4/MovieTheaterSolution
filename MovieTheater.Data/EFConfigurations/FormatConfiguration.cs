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
    public class FormatConfiguration : IEntityTypeConfiguration<Format>
    {
        public void Configure(EntityTypeBuilder<Format> builder)
        {
            builder.ToTable("Formats");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();

        }
    }
}
