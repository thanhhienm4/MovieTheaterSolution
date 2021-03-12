using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MovieTheater.Data.EF
{

    public class MovieTheaterDBContextFactory : IDesignTimeDbContextFactory<MovieTheaterDBContext>
    {
        public MovieTheaterDBContext CreateDbContext(string []Args)
        {
            string workingDirectory = Environment.CurrentDirectory;         
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(workingDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var ConnectionString = configuration.GetConnectionString("MovieTheaterDBContext");
            
            var optionsBuilder = new DbContextOptionsBuilder<MovieTheaterDBContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            return new MovieTheaterDBContext(optionsBuilder.Options);
        }

       
    }
}
