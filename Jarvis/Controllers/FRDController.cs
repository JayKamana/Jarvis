using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jarvis.Models;
using Jarvis.ViewModel;
using Microsoft.AspNet.Identity;

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


            if (User.IsInRole(RoleName.CanManageFRD))
                return View("List", frds);

            return View("ReadOnlyList", frds);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FRD frd = _context.FRDS.Include(f => f.User).SingleOrDefault(f => f.Id == id);
            if (frd == null)
            {
                return HttpNotFound();
            }
            return View(frd);
        }

        public ActionResult Approve(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FRD frd = _context.FRDS.Include(f => f.User).Include(d => d.User.Department).Include(m => m.User.Department.Manager).SingleOrDefault(f => f.Id == id);
            if (frd == null)
            {
                return HttpNotFound();
            }

            return View(frd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(FRDApproveMessage model)
        {
            if (ModelState.IsValid)
            {

                var ManagerEmail = _context.Users.FirstOrDefault(u => u.Id == model.ManagerID).Email;

                var body = "<h3>Request For FRD Approval</h3><p>The FRD <strong>" + model.FrdName + "</strong> by <strong>" + model.DemandOwnerEmail + "</strong> is currently awaiting your approval</p><p><a href='https://localhost:44391/frd/confirm/" + model.FrdId + "'>View FRD</a></p>";
                var message = new MailMessage();  
                message.To.Add(new MailAddress(ManagerEmail));

                message.From = new MailAddress("cmpe406gradproject@outlook.com");  
  
                message.Subject = "FRD Approval";
                message.Body = string.Format(body, model.Message, model.FrdId, model.DemandOwnerEmail, model.FrdName, model.ManagerID);
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.Default;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "cmpe406gradproject@outlook.com",  
                        Password = "c05L!vyCwKBL"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FRD frd = _context.FRDS.Include(f => f.User).SingleOrDefault(f => f.Id == id);
            if (frd == null)
            {
                return HttpNotFound();
            }
            
            return View(frd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "Id,Name,UserId,isApproved")] FRD frd)
        {
            
            if (ModelState.IsValid)
            {
                _context.Entry(frd).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frd);
        }

    }
}