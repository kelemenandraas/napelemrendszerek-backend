using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
namespace napelemrendszerek_backend.Models
{
    class ProjectStates
    {
        //attributes
        [Key]
        public int projectStateID { get; set; }

        [MaxLength(10)]
        public string projectStateName { get; set; }

        //connection
        public ICollection<Project> Projects { get; set; }
        public ICollection<ProjectHistory> ProjectHistories { get; set; }

    }
}
