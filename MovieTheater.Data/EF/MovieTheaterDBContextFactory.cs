using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace MovieTheater.Data.EF
{
    public class MovieTheaterDbContextFactory : IDesignTimeDbContextFactory<MovieTheaterDBContext>
    {
        public MovieTheaterDBContext CreateDbContext(string[] Args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(workingDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("MovieTheaterDBContext");

            var optionsBuilder = new DbContextOptionsBuilder<MovieTheaterDBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MovieTheaterDBContext(optionsBuilder.Options);
        }
    }
}