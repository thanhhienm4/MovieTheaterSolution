﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EFConfig;
using MovieTheater.Data.EFConfigurations;
using MovieTheater.Data.Entities;
using MovieTheater.Data.Extensions;
using System;

namespace MovieTheater.Data.EF
{
    public class MovieTheaterDBContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<AppUser> AppUsers { get; set; }
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
        public DbSet<IdentityUserRole<Guid>> AppUserRoles { get; set; }

        public MovieTheaterDBContext(DbContextOptions<MovieTheaterDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
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
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new FilmInGenreConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new KindOfScreeningConfiguration());


            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.Seed();
        }
    }
}