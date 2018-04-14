using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int ItemNumber { get; set; }

        public DateTime DateAdded { get; set; }

        public string Details { get; set; }

        public int FRDId { get; set; }
        public FRD FRD { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

