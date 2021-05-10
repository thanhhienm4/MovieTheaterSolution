using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class People
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Joining> Joinings { get; set; }
    }
}