﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class FRDApproveMessage
    {

        public int FrdId { get; set; }
        public string FrdName { get; set; }
        public string UserId { get; set; }
        public string DemandOwnerEmail { get; set; }
        public string ManagerID { get; set; }
    }
}