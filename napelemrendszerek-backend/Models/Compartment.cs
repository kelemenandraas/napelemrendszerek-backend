using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Compartment
    {
        public int CompartmentId { get; set; }
        public int CompartmentRow { get; set; }
        public int CompartmentCulomn { get; set; }
        public int CompartmentCell { get; set; }
        public int StoredAmount { get; set; }
        public string StoredPartName { get; set; }

        public virtual Part StoredPartNameNavigation { get; set; }
    }
}
