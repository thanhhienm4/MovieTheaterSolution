using MovieTheater.Data.Enums;
using MovieTheater.Models.Common;

namespace MovieTheater.Models.User
{
    public class UserPagingRequest : PagingRequest
    {
        public string RoleId { get; set; }
        public Status? Status { get; set; }
    }
}