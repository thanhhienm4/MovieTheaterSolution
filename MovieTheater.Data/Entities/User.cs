using Microsoft.AspNetCore.Identity;
using System;

namespace MovieTheater.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public UserInfor UserInfor { get; set; }
        public CustomerInfor CustomerInfor { get; set; }
    }
}