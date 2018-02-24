using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jarvis.Models;
using Jarvis.ViewModel;

namespace Jarvis.Controllers
{
    public class FRDController : Controller
    {
        private ApplicationDbContext _context;

        public FRDController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var frds = _context.FRDS.Include(c => c.User).ToList();
            if(User.IsInRole(RoleName.CanManageFRD))
                return View("List", frds);

            return View("ReadOnlyList", frds);
        }

    }
}