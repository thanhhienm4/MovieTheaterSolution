using MovieTheater.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Identity.Role
{
    public class RoleAssignRequest
    {
        public Guid UserId { get; set; }
        public List<SelectedItem> Roles { get; set;}

        public RoleAssignRequest()
        {
            Roles = new List<SelectedItem>();
        }
    }
}
