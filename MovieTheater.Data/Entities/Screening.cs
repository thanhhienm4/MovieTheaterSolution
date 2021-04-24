using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public bool Active { get; set; }

        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<Ticket> Tickets { get; set; }
        
        public int KindOfScreeningId { get; set; }
        public KindOfScreening KindOfScreening { get; set; }
    }
}