using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class FRDChannelMapping
    {
        public int Id { get; set; }
        public int FRDId { get; set; }
        public int ChannelId { get; set; }

        public virtual FRD FRD { get; set; }
        public virtual Channel Channel { get; set; }
    }
}