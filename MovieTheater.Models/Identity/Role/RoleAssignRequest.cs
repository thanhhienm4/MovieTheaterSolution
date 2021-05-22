using MovieTheater.Models.Common;
using System;
using System.Collections.Generic;

namespace MovieTheater.Models.Identity.Role
{
    public class RoleAssignRequest
    {
        public Guid UserId { get; set; }
        public List<SelectedItem> Roles { get; set; }

        public RoleAssignRequest()
        {
            Roles = new List<SelectedItem>();
        }
    }
}