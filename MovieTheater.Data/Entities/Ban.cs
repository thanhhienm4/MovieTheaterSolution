using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class Ban
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<Film> Films { get; set; }
    }
}