using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jarvis.Models
{
    public class SMSDemandFRD
    {

        [Display(Name = "FRD")]
        [Key]
        [Column(Order = 1)]
        public int FRDId { get; set; }
        public virtual FRD FRD { get; set; }

        [Display(Name = "SMS Demand")]
        [Key]
        [Column(Order = 2)]
        public int SMSDemandId { get; set; }
        public virtual SMSDemand SMSDemand { get; set; }
    }
}