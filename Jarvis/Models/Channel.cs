using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Channel
    {
        public Channel()
        {
            this.FRDS = new HashSet<FRD>();
        }

        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public virtual ICollection<FRD> FRDS { get; set; }
        public virtual ICollection<FRDChannelMapping> FRDChannelMappings { get; set; }

    }
}