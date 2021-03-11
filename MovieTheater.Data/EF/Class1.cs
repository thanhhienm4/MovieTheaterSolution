using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Data.EF
{

    public class MovieTheaterDBContextFactory : IDesignTimeDbContextFactory<MovieTheaterDBContext>
    {
        public MovieTheaterDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var ConnectionString = configuration.GetConnectionString("MovieTheaterDBContext");
            //var ConnectionString = "Server=DESKTOPM4\\SQLEXPRESS;Database=MovieTheaterManagerment;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<MovieTheaterDBContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            return new MovieTheaterDBContext(optionsBuilder.Options);
        }
    }
}
