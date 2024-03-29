﻿using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class KindOfSeat
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Surcharge { get; set; }

        public List<Seat> Seats { get; set; }
    }
}