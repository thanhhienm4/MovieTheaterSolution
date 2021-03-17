using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class RoomFormat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public List<Room> Rooms { get; set; }
    }
}