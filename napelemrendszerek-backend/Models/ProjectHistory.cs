using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class ProjectHistory
    {
        //attributes
        public DateTime date { get; set; }

        //connection
        public int oldProjectStateID { get; set; }

        public int newProjectStateID { get;set; }
    }
}
