using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class FRDAudienceMapping
    {
        public int Id { get; set; }

        public int FRDId { get; set; }
        public int TargetAudienceId { get; set; }

        public int AudienceNumber { get; set; }

        public virtual FRD FRD { get; set; }
        public virtual TargetAudience TargetAudiences { get; set; }
    }
}