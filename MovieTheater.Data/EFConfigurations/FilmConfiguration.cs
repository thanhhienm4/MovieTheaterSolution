using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTheater.Data.Entities;

namespace MovieTheater.Data.EFConfigurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.Ban).WithMany(x => x.Films).HasForeignKey(x => x.BanId);
        }
    }
}