using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jarvis.ViewModel
{
    public class FRDviewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The FRD name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a FRD name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z'-'\s]*$", ErrorMessage = "Please enter a FRD name made up of letters and numbers only")]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "The reference number cannot be blank")]
        [Range(0, 10000, ErrorMessage = "Please enter a reference number")]
        [RegularExpression(@"^[0-9\s]*$", ErrorMessage = "Please enter a reference number made up of numbers only")]
        public int ReferenceNumber { get; set; }

    
        public List<SelectList> ChannelLists { get; set; }
        public string[] Channels { get; set; }

        public List<SelectList> AudienceLists { get; set; }
        public string[] TargetAudiences { get; set; }
    
    }
}