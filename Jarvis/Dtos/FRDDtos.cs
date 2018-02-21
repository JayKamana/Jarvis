using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Jarvis.Models;

namespace Jarvis.Dtos
{
    public class FRDDtos
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }


        [Required]
        public int EmployeeId { get; set; }
    }
}