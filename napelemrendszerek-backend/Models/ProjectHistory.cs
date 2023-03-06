using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class ProjectHistory
    {
        public int ProjectId { get; set; }
        public int OldProjectStateId { get; set; }
        public int NewProjectStateId { get; set; }
        public DateTime DateOfChange { get; set; }

        public virtual ProjectStates NewProjectState { get; set; }
        public virtual ProjectStates OldProjectState { get; set; }
        public virtual Project Project { get; set; }
    }
}
