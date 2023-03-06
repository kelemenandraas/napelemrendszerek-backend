using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Users
    {
        public Users()
        {
            Project = new HashSet<Project>();
        }

        public string Username { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}
