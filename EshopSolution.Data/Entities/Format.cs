using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
