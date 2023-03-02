using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.DBModels
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int ProjectStateId { get; set; }
        public int CreatedBy { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ProjectLocation { get; set; }
        public string ProjectDescription { get; set; }
        public int WorkFee { get; set; }
        public int EstimatedTimeInDays { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual ProjectStates ProjectState { get; set; }
    }
}
