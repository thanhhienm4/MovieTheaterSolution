
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class UserCreateVMD
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public String Password { get; set; }
    }
}
