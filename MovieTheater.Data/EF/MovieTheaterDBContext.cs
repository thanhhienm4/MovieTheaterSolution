using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Data.EF
{
    public class MovieTheaterDBContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<KindOfFilm> KindOfFilms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationType> ReservationTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


    }
}
