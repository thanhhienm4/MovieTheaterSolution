using System;

namespace MovieTheater.Models.Identity.Role
{
    public class RoleUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}