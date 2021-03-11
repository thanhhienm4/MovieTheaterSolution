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
    class JoiningConfiguration : IEntityTypeConfiguration<Joining>
    {
        public void Configure(EntityTypeBuilder<Joining> builder)
        {
            builder.ToTable("Joinings");
            builder.HasKey(x => new { x.FilmId, x.PeppleId, x.PositionId });

        }
    }
}
