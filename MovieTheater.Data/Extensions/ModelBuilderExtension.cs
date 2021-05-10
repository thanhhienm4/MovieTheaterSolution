using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Entities;
using System;

namespace MovieTheater.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = new Guid("1081FBA0-8368-43B7-8134-032E838C1BB3"),
                    Name = "Employee",
                    NormalizedName = "Employee",
                    Description = "Employee role"
                },
                new AppRole()
                {
                    Id = new Guid("C02AB224-EBDD-44E3-B691-5ACEC03DA039"),
                    Name = "Admin",
                    NormalizedName = "Administrator",
                    Description = "Administrator role"
                },
                 new AppRole()
                 {
                     Id = new Guid("0417C463-9AF0-46D9-9FF7-D3E63321DFCC"),                     
                     Name = "Customer",
                     NormalizedName = "Customer",
                     Description = "Customer role"
                 });

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = new Guid("99ECA8CE-E954-4ED9-AB12-1A1FB010A9F8"),
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "Mistake4@gmail.com",
                    NormalizedEmail = "Mistakem4@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin123"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = string.Empty,
                    PhoneNumber = "0912413908",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    LockoutEnd = new DateTimeOffset(),
                   
                });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData
                (
                    new IdentityUserRole<Guid>()
                    {
                        RoleId = new Guid("C02AB224-EBDD-44E3-B691-5ACEC03DA039"),  
                        UserId = new Guid("99ECA8CE-E954-4ED9-AB12-1A1FB010A9F8")
                    }
                ); 
            
            modelBuilder.Entity<UserInfor>().HasData(
                 new UserInfor()
                 {
                     Id = new Guid("99ECA8CE-E954-4ED9-AB12-1A1FB010A9F8"),

                     FirstName = "Hien",
                     LastName = "Nguyen Thanh",
                     Dob = new DateTime(2020, 01, 31),
                 })  ;
            modelBuilder.Entity<RoomFormat>().HasData(
                new RoomFormat()
                {
                    Id = 1,
                    Name = "3D",
                    Price = 80000
                },
                new RoomFormat()
                {
                    Id = 2,
                    Name = "2D",
                    Price = 60000
                }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room()
                {
                    Id = 1,
                    Name = "1",
                    FormatId = 1
                });
            modelBuilder.Entity<KindOfSeat>().HasData(
                new KindOfSeat()
                {
                    Id = 1,
                    Name = "Thường"
                },
                new KindOfSeat()
                {
                    Id = 2,
                    Name = "Vip"
                });
            modelBuilder.Entity<SeatRow>().HasData(
                new SeatRow()
                {
                    Id = 1,
                    Name = 'A'.ToString()
                },
                 new SeatRow()
                 {
                     Id = 2,
                     Name = 'B'.ToString()
                 },
                 new SeatRow()
                 {
                     Id = 3,
                     Name = 'C'.ToString()
                 }
                ) ;
           
            modelBuilder.Entity<Seat>().HasData(
              new Seat()
              {
                  Id = 1,
                  KindOfSeatId = 1,
                  Number = 1,
                  RoomId = 1,
                  RowId = 1
              });
            
            modelBuilder.Entity<Ban>().HasData(
                new Ban()
                {
                    Id = 1,
                    Name = "18+"
                },
                new Ban()
                {
                    Id = 2,
                    Name = "13+"
                });
            modelBuilder.Entity<FilmGenre>().HasData(
                new FilmGenre()
                {
                    Id = 1,
                    Name = "Hành động"
                },
                new FilmGenre()
                {
                    Id = 2,
                    Name = "Haì"
                });
            
            modelBuilder.Entity<People>().HasData(
                new People()
                {
                    Id = 1,
                    Name = "Tom Cruise",
                    Description = "homas Cruise Mapother IV là một nam diễn viên và nhà sản xuất người Mỹ. Anh bắt đầu sự nghiệp của mình ở tuổi 19 với bộ phim Endless Love, trước khi nhận được sự chú ý từ công chúng với vai diễn Trung úy Pete \"Maverick\" Mitchell trong Top Gun",
                    DOB = new DateTime(1962, 7, 3)
                }
                );
            modelBuilder.Entity<Film>().HasData(
                new Film()
                {
                    Id = 1,
                    Name = "The Mummy",
                    BanId = 1,
                    Description = "Xác ướp(tên gốc tiếng Anh: The Mummy) là một bộ phim điện ảnh phiêu lưu - hành động của Mỹ năm 2017[9][10] do Alex Kurtzman đạo diễn và David Koepp, Christopher McQuarrie cùng Dylan Kussman thực hiện phần kịch bản, dựa trên cốt truyện gốc của Kurtzman, Jon Spaihts và Jenny Lumet.",
                });
            modelBuilder.Entity<KindOfScreening>().HasData(
                new KindOfScreening()
                {
                    Id = 1,
                    Name = "Chiếu sớm",
                    Surcharge = 20000
                },
                new KindOfScreening()
                {
                    Id = 2,
                    Name = "Bình thường",
                    Surcharge = 0
                }) ;
            modelBuilder.Entity<Screening>().HasData(
              new Screening()
              {
                  Id = 1,
                  FilmId = 1,
                  KindOfScreeningId = 1,
                  RoomId = 1,
                 // Surcharge = 20000,
                  StartTime = DateTime.UtcNow
              });
            modelBuilder.Entity<ReservationType>().HasData(
                new ReservationType()
                {
                    Id = 1,
                    Name = "Đặt trực tiếp"
                },
                new ReservationType()
                {
                    Id = 2,
                    Name = "Đặt qua mạng "
                });
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation()
                {
                    Id = 1,
                    ReservationTypeId = 1,
                    EmployeeId = new Guid("99ECA8CE-E954-4ED9-AB12-1A1FB010A9F8"),
                    CustomerId = null
                    

                });
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket()
                {
                    ScreeningId = 1,
                    SeatId = 1,
                    ReservationId = 1


                }
                );
            modelBuilder.Entity<Position>().HasData(
                new Position()
                {
                    Id = 1,
                    Name = "Diễn viên"


                },
                new Position()
                {
                    Id = 2,
                    Name = "Đạo diễn"


                }
                );

        }
    }
}