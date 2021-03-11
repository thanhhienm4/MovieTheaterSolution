using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EFConfig;
using MovieTheater.Data.EFConfigurations;
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
        public DbSet<Joining> Joinings { get; set; }
        public DbSet<KindOfSeat> KindOfSeats { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public MovieTheaterDBContext (DbContextOptions<MovieTheaterDBContext> options) : base(options) { } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BanConfiguration());
            modelBuilder.ApplyConfiguration(new FormatConfiguration());
            modelBuilder.ApplyConfiguration(new JoiningConfiguration());
            modelBuilder.ApplyConfiguration(new KindOfFilmConfiguration());
            modelBuilder.ApplyConfiguration(new KindOfSeatConfiguration());
            modelBuilder.ApplyConfiguration(new PeopleConfiguaration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ScreeningConfiguration());
            modelBuilder.ApplyConfiguration(new SeatConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
