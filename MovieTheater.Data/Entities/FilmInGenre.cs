namespace MovieTheater.Data.Entities
{
    public class FilmInGenre
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int FilmGenreId { get; set; }
        public FilmGenre FilmGenre { get; set; }
    }
}