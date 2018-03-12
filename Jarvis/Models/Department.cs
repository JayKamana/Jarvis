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

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}