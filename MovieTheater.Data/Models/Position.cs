using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Position
    {
        public Position()
        {
            Joinings = new HashSet<Joining>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Joining> Joinings { get; set; }
    }
}