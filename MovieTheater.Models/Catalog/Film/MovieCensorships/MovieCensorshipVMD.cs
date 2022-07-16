using MovieTheater.Data.Models;

namespace MovieTheater.Models.Catalog.Film.MovieCensorships
{
    public class MovieCensorshipVMD
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MovieCensorshipVMD(MovieCensorship movieCensorship)
        {
            if (movieCensorship != null)
            {
                Id = movieCensorship.Id;
                Name = movieCensorship.Name;
            }
                
               
        }
    }
}