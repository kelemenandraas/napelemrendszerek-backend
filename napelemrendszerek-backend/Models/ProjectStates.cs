using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class ProjectStates
    {
        public ProjectStates()
        {
            Project = new HashSet<Project>();
        }

        public int ProjectStateId { get; set; }
        public string ProjectStateName { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
