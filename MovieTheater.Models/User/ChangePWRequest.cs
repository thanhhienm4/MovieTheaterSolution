using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class ChangePWRequest
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string  NewPassword { get; set; }
    }
}
