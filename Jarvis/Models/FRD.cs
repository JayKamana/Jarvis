using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class FRD
    {
        public FRD()
        {
            this.SMSDemands = new HashSet<SMSDemand>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<SMSDemand> SMSDemands { get; set; }

    }
}