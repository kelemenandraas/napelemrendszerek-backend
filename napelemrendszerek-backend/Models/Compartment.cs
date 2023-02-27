using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class Compartment
    {
        //attributes
        [Key]
        public int compartmentID { get; set; }

        public int compartmentRow { get; set; }
        public int compartmentCulomn { get; set; }
        public int compartment { get; set; }

        //connection
        public Part Part { get; set; }
        public int storedPartID { get; set; }
    }
}
