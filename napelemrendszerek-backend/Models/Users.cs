using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace napelemrendszerek_backend.Models
{
    class Users
    {
        //attributes
        [Key]
        public int userId { get; set; }

        [MaxLength(20)]
        public string username { get; set; }

        [MaxLength(100)]
        public string userPassword { get; set; }

        //connection
        public Roles Role { get; set; }
        public int roleID { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
