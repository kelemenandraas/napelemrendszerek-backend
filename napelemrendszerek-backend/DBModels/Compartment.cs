using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.DBModels
{
    public partial class Compartment
    {
        public int CompartmentId { get; set; }
        public int CompartmentRow { get; set; }
        public int CompartmentCulomn { get; set; }
        public int Compartment1 { get; set; }
        public int StoredPartId { get; set; }

        public virtual Part StoredPart { get; set; }
    }
}
