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
        }
    }
}