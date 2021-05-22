using System.Collections.Generic;

namespace MovieTheater.Models.Catalog.Film
{
    public class JoiningVMD
    {
        public string Name { get; set; }
        public int PosId { get; set; }
        public int FilmId { get; set; }
        public int PeopleId { get; set; }
    }

    public class JoiningPosVMD
    {
        public string Name { get; set; }
        public List<JoiningVMD> Joinings { get; set; }
    }
}