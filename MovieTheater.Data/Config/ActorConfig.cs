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
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("Actor");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.Dob).HasColumnName("DOB");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(156);
        }
    }
}
