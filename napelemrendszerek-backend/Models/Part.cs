using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class Part
    {
        //attributes
        [Key]
        public int partID { get; set; }

        [MaxLength(50)]
        public string partName { get; set; }


        public int maxNumberInBox { get; set; }
        public int sellPrice { get; set; }
        public int numberAvailable { get; set; }

        //connection
        public ICollection<Compartment> Compartments { get; set; }
    }
}
