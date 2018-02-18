using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jarvis.Models;

namespace Jarvis.ViewModel
{
    public class FRDDisplayViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<FRD> FRDS { get; set; }
    }
}