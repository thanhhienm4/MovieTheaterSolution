using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Price.Time
{
    public class TimeVMD
    {
        public string TimeId { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Name { get; set; }
    }
}
