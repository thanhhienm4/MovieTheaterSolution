using System;
using System.Collections.Generic;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            staff = new HashSet<staff>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
