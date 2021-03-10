using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public List<AppUser> Users { get; set; }

    }
}
