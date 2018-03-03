﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class SMSCode
    {
        public int Id { get; set; }

        [Required]
        public string CodeDescription { get; set; }
        public virtual ICollection<SMSDemand> SMSDemand { get; set; }
    }
}