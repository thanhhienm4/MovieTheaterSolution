using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FormatId { get; set; }
        public RoomFormat Format { get; set; }

        public List<SeatRow> SeatRows { get; set; }
        public List<Screening> Screenings { get; set; }
    }
}