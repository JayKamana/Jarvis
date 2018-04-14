using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public string Details { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }
}
