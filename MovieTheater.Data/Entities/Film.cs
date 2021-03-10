using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheater.Data.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public string Producer { get; set; }
        public int Length { get; set; }

        public int BanId { get; set; }
        public Ban Ban { get; set; }

        public int KindOfFilmId { get; set; }
        public KindOfFilm KindOfFilm { get; set; }

        public List<Screening> Screenings { get; set; }





    }
}
