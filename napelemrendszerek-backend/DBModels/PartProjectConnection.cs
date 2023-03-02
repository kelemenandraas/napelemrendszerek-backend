using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.DBModels
{
    public partial class PartProjectConnection
    {
        public int ProjectId { get; set; }
        public int PartId { get; set; }
        public int NumberReserved { get; set; }
        public int PartStateId { get; set; }

        public virtual Part Part { get; set; }
        public virtual PartStates PartState { get; set; }
        public virtual Project Project { get; set; }
    }
}
