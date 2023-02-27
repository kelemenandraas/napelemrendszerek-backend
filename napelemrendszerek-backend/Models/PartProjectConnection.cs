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

        public ICollection<Part> Parts { get; set; }

        public ICollection<PartStates> PartStates { get; set; }

    }
}
