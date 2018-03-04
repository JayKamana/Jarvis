using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class SMSDemandFRD
    {
        public int ID { get; set; }

        public int FRDId { get; set; }
        public virtual FRD FRD { get; set; }


        public int SMSDemandId { get; set; }
        public virtual SMSDemand SMSDemand { get; set; }
    }
}