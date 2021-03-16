namespace MovieTheater.Data.Entities
{
    public class Joining
    {
        public int PeppleId { get; set; }
        public People People { get; set; }

        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}