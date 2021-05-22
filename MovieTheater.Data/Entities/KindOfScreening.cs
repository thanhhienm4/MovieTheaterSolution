using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class KindOfScreening
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Surcharge { get; set; }

        public List<Screening> Screenings { get; set; }
    }
}