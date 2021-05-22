using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EFConfig;
using MovieTheater.Data.EFConfigurations;
using MovieTheater.Data.Entities;
using MovieTheater.Data.Extensions;
using System;
using System.Linq;

namespace MovieTheater.Data.EF
{
    public class MovieTheaterDBContext : IdentityDbContext<User, AppRole, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<RoomFormat> RoomFormats { get; set; }
        public DbSet<FilmGenre> FilmGenre { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationType> ReservationTypes { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Joining> Joinings { get; set; }
        public DbSet<KindOfSeat> KindOfSeats { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<FilmInGenre> FilmInGenres { get; set; }
        public DbSet<KindOfScreening> KindOfScreenings { get; set; }
        public DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }
        public DbSet<UserInfor> UserInfors { get; set; }
        public DbSet<CustomerInfor> CustomerInfors { get; set; }
        public DbSet<SeatRow> SeatRows { get; set; }

        public MovieTheaterDBContext(DbContextOptions<MovieTheaterDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerInforConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserInforConfiguration());
            modelBuilder.ApplyConfiguration(new BanConfiguration());
            modelBuilder.ApplyConfiguration(new RoomFormatConfiguration());
            modelBuilder.ApplyConfiguration(new JoiningConfiguration());
            modelBuilder.ApplyConfiguration(new FilmGenreConfiguration());
            modelBuilder.ApplyConfiguration(new KindOfSeatConfiguration());
            modelBuilder.ApplyConfiguration(new PeopleConfiguaration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ScreeningConfiguration());
            modelBuilder.ApplyConfiguration(new SeatConfiguration());
            modelBuilder.ApplyConfiguration(new SeatRowConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new FilmInGenreConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new KindOfScreeningConfiguration());

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Seed();
        }
    }
}