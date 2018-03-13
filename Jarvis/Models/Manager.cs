using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Manager
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Department> Department  { get; set; }
    }
}