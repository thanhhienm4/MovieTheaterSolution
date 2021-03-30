using MovieTheater.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class UserVMD
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public string UserName { get; set;}

        public Status Status { get; set; }

        public List<string> Roles { get; set; }

    }
}
