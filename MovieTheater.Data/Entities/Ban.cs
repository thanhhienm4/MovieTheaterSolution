using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Ban
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public List<Film> Films { get; set; }
    }
}
