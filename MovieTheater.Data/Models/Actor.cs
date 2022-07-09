using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Joinings = new HashSet<Joining>();
        }

        public int Id { get; set; }
        public DateTime Dob { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Joining> Joinings { get; set; }
    }
}
