using MovieTheater.Data.Enums;
using MovieTheater.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.User
{
    public class UserPagingRequest : PagingRequest
    {
        public string  RoleId { get; set; }
        public Status Status { get; set; }
    }
}
