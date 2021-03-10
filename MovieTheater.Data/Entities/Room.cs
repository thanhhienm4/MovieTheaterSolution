using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string FormatId { get; set; }
        public Format Format { get; set; }

        public List<Seat> Seats { get; set; }
        public List<Screening> Screenings { get; set; }
    }
}
