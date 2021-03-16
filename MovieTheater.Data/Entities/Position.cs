using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Joining> Joinings { get; set; }
    }
}