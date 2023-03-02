using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.DBModels
{
    public partial class Part
    {
        public Part()
        {
            Compartment = new HashSet<Compartment>();
        }

        public int PartId { get; set; }
        public string PartName { get; set; }
        public int MaxNumberInBox { get; set; }
        public int SellPrice { get; set; }
        public int NumberAvailable { get; set; }

        public virtual ICollection<Compartment> Compartment { get; set; }
    }
}
