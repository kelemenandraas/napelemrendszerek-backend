using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class Roles
    {
        //attributes
        [Key]
        public int roleID { get; set; }

        [MaxLength(15)]
        public string roleName { get; set; }

        //connection
        public ICollection<Users> Users { get; set; }
    }
}
