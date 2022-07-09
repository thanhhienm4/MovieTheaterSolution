using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MovieTheater.Data.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Auditorium> Auditoria { get; set; }
        public virtual DbSet<AuditoriumFormat> AuditoriumFormats { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Joining> Joinings { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieCensorship> MovieCensorships { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<MovieInGenre> MovieInGenres { get; set; }
        public virtual DbSet<PaymentStastu> PaymentStastus { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationPerson> ReservationPeople { get; set; }
        public virtual DbSet<ReservationType> ReservationTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<SeatRow> SeatRows { get; set; }
        public virtual DbSet<SeatType> SeatTypes { get; set; }
        public virtual DbSet<Surchange> Surchanges { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketPrice> TicketPrices { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Hienm4;Database=Movies;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(156);
            });

            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.ToTable("Auditorium");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FormatId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.Auditoria)
                    .HasForeignKey(d => d.FormatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auditorium_AuditoriumFormat");
            });

            modelBuilder.Entity<AuditoriumFormat>(entity =>
            {
                entity.ToTable("AuditoriumFormat");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("Customer");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("CustomerType");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Joining>(entity =>
            {
                entity.HasKey(e => new { e.ActrorId, e.MovieId, e.PositionId });

                entity.Property(e => e.MovieId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PositionId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Actror)
                    .WithMany(p => p.Joinings)
                    .HasForeignKey(d => d.ActrorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Joinings_Actor");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Joinings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Joinings_Movies");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Joinings)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Joinings_Position");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CensorshipId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.Property(e => e.TrailerUrl).HasColumnName("TrailerURL");

                entity.HasOne(d => d.Censorship)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.CensorshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_MovieCensorship");
            });

            modelBuilder.Entity<MovieCensorship>(entity =>
            {
                entity.ToTable("MovieCensorship");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.ToTable("MovieGenre");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MovieInGenre>(entity =>
            {
                entity.HasKey(e => new { e.GenreId, e.MovieId });

                entity.ToTable("MovieInGenre");

                entity.Property(e => e.GenreId)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.MovieId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MovieInGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieInGenre_MovieGenre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieInGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieInGenre_Movies");
            });

            modelBuilder.Entity<PaymentStastu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.Customer)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK_Reservation_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Reservation_Staff");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_PaymentStastus");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ScreeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Screening");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_ReservationType");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Voucher");
            });

            modelBuilder.Entity<ReservationPerson>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CustomerTypeId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.ReservationPeople)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationPeople_CustomerType");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationPeople)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationPeople_Reservation");
            });

            modelBuilder.Entity<ReservationType>(entity =>
            {
                entity.ToTable("ReservationType");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.ToTable("Screening");

                entity.Property(e => e.AuditoriumId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.MovieId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.AuditoriumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screening_Auditorium");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screening_Movies");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.Property(e => e.AuditoriumId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.AuditoriumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seat_Auditorium");

                entity.HasOne(d => d.Row)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.RowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seat_SeatRow");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seat_SeatType");
            });

            modelBuilder.Entity<SeatRow>(entity =>
            {
                entity.ToTable("SeatRow");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SeatType>(entity =>
            {
                entity.ToTable("SeatType");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Surchange>(entity =>
            {
                entity.ToTable("Surchange");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AuditoriumId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SeatType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Surchanges)
                    .HasForeignKey(d => d.AuditoriumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surchange_Auditorium");

                entity.HasOne(d => d.SeatTypeNavigation)
                    .WithMany(p => p.Surchanges)
                    .HasForeignKey(d => d.SeatType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Surchange_SeatType");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ReservationId)
                    .HasConstraintName("FK_Ticket_Reservation");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Seat");
            });

            modelBuilder.Entity<TicketPrice>(entity =>
            {
                entity.ToTable("TicketPrice");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AuditoriumFormat)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerType)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TimeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToTime).HasColumnType("datetime");

                entity.HasOne(d => d.AuditoriumFormatNavigation)
                    .WithMany(p => p.TicketPrices)
                    .HasForeignKey(d => d.AuditoriumFormat)
                    .HasConstraintName("FK_TicketPrice_AuditoriumFormat");

                entity.HasOne(d => d.CustomerTypeNavigation)
                    .WithMany(p => p.TicketPrices)
                    .HasForeignKey(d => d.CustomerType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketPrice_CustomerType");

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.TicketPrices)
                    .HasForeignKey(d => d.TimeId)
                    .HasConstraintName("FK_TicketPrice_Time");
            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.ToTable("Time");

                entity.Property(e => e.TimeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2550)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MaxValue).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Operator)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("Staff");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
