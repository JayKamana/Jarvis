using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public bool CanApproveFRD { get; set; }

        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}