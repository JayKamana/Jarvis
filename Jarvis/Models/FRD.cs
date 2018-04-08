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
            this.Channels = new HashSet<Channel>();
            this.TargetAudiences = new HashSet<TargetAudience>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }

        [Display(Name = "FRD Number")]
        public int ReferenceNumber { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name ="Approved")]
        public bool? isApproved { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<SMSDemand> SMSDemands { get; set; }
        public virtual ICollection<TargetAudience> TargetAudiences { get; set; }
        public virtual ICollection<Channel> Channels { get; set; }
    }
}