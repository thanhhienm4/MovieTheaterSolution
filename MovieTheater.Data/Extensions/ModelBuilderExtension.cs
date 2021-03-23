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
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "Administrator",
                    Description = "Administrator role"
                });
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "Mistake4@gmail.com",
                    NormalizedEmail = "Mistakem4@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin123"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = string.Empty,

                    FirstName = "Hien",
                    LastName = "Nguyen Thanh",
                    Dob = new DateTime(2020, 01, 31),
                    PhoneNumber = "0912413908",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    LockoutEnd = new DateTimeOffset()
                });
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
                });
            modelBuilder.Entity<Ban>().HasData(
                new Ban()
                {
                    Id = 1,
                    Name = "18+"
                });
            modelBuilder.Entity<FilmGenre>().HasData(
                new FilmGenre()
                {
                    Id = 1,
                    Name = "Hành động"
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
                }) ;

        }
    }
}