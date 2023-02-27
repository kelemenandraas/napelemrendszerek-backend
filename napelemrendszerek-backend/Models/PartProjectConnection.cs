using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class PartProjectConnection
    {
        //attribute
        public int numberReserved { get; set; }

        //conenction
        public Project Project { get; set; }
        public int projectID { get; set; }

        public Part Part { get; set; }
        public int partID { get; set; }

        public PartStates PartState { get; set; }
        public int partStateID { get; set; }

    }
}
