using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Config;
using MovieTheater.Data.Results;

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

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Auditorium> Auditoriums { get; set; }
        public virtual DbSet<AuditoriumFormat> AuditoriumFormats { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Joining> Joinings { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieCensorship> MovieCensorships { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<MovieInGenre> MovieInGenres { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationType> ReservationTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<SeatRow> SeatRows { get; set; }
        public virtual DbSet<SeatType> SeatTypes { get; set; }
        public virtual DbSet<Surcharge> Surcharges { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketPrice> TicketPrices { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<staff> Staffs { get; set; }
        public virtual DbSet<SeatModel> SeatModel { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=Hienm4;Database=Movies;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<SeatModel>(e => { e.HasNoKey(); });
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new MovieCensorshipConfig());
            modelBuilder.ApplyConfiguration(new MovieGenreConfig());
            modelBuilder.ApplyConfiguration(new ActorConfig());
            modelBuilder.ApplyConfiguration(new AuditoriumConfig());
            modelBuilder.ApplyConfiguration(new AuditoriumFormatConfig());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfig());
            modelBuilder.ApplyConfiguration(new JoiningConfig());
            modelBuilder.ApplyConfiguration(new MovieConfig());
            modelBuilder.ApplyConfiguration(new MovieInGenreConfig());
            modelBuilder.ApplyConfiguration(new PaymentStatusConfig());
            modelBuilder.ApplyConfiguration(new PositionConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new ReservationTypeConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ScreeningConfig());
            modelBuilder.ApplyConfiguration(new SeatConfig());
            modelBuilder.ApplyConfiguration(new SeatRowConfig());
            modelBuilder.ApplyConfiguration(new SeatTypeConfig());
            modelBuilder.ApplyConfiguration(new SurchargeConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new TicketPriceConfig());
            modelBuilder.ApplyConfiguration(new TimeConfig());
            modelBuilder.ApplyConfiguration(new VoucherConfig());
            modelBuilder.ApplyConfiguration(new StaffConfig());
            modelBuilder.ApplyConfiguration(new InvoiceConfig());
            modelBuilder.ApplyConfiguration(new PaymentConfig());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}