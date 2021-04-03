using Microsoft.AspNetCore.Identity;
using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;

namespace MovieTheater.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public UserInfor UserInfor { get; set; }
        
    }
}