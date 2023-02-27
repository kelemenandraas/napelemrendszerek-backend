using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class PartStates
    {
        //attributes
        [Key]
        public int partStateID { get; set; }

        [MaxLength(10)]
        public string partStateName { get; set; }

        //connection
    }
}
