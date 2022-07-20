using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class 
        Time
    {
        public Time()
        {
            TicketPrices = new HashSet<TicketPrice>();
        }

        public string TimeId { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
        public int DateStart { get; set; }
        public int DateEnd { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<TicketPrice> TicketPrices { get; set; }
    }
}
