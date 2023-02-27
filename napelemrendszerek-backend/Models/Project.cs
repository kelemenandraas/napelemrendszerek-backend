using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using napelemrendszerek_backend.Models;

namespace napelemrendszerek_backend.Models
{
    class Project
    {
        //attributes
        [Key]
        public int projectID { get; set; }

        [MaxLength(30)]
        public string customerName { get; set; }
        [MaxLength(50)]
        public string customerAddress { get; set; }
        [MaxLength(20)]
        public string customerPhone { get; set; }
        [MaxLength(30)]
        public string customerEmail { get; set; }


        public DateTime startDate { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public DateTime? closedDate { get; set; }

        [MaxLength(50)]
        public string projectLocation { get; set; }
        [MaxLength(50)]
        public string projectDescription { get; set; }

        public int workFee { get; set; }
        public int estimatedTimeInDays { get; set; }
        //connection
        public ProjectStates ProjectState { get; set; }
        public int projectStateID { get; set; }

        public Users User { get; set; }
        public int createdBy { get; set; }

        public ICollection<PartProjectConnection> partProjectConnections { get; set; }

        public ICollection<ProjectHistory> projectHistory { get; set; }

    }
}
