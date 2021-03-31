using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Catalog.Film
{
    public class PeopleVMD
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
