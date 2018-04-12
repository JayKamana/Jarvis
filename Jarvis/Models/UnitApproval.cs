using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Models
{
    public class UnitApproval
    {
        public int Id { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string ApprovedBy { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public int FRDId { get; set; }
        public virtual FRD FRD { get; set; }
    }
}
