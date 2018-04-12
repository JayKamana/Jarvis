using Jarvis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.ViewModel
{
    public class FRDDetailsViewModel
    {
        public IEnumerable<FRD> FRDS { get; set; }

        public IEnumerable<UnitApproval> UnitApprovals { get; set; }

    }
}