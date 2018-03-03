using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jarvis.Models
{
    public class SMSDemand
    {
        public int ID { get; set; }

        [Required]
        public string Sender { get; set; }


        public string Content_tr { get; set; }


        public string Content_en { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int FRDId { get; set; }
        public virtual FRD FRD { get; set; }

        public int SMSCodeId { get; set; }
        public virtual SMSCode SMSCode { get; set; }
    }
}