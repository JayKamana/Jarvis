using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FName { get; set; }

        [Required]
        [StringLength(255)]
        public string LName { get; set; }
    }
}