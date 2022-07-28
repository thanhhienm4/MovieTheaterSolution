using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;

namespace MovieTheater.Models.User
{
    public class UserVMD
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public string UserName { get; set; }

        public Status Status { get; set; }

        public string Role { get; set; }
    }
}