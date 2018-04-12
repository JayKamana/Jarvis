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
            var userId = User.Identity.GetUserId();

            var frds = _context.FRDS.Include(c => c.User).ToList();

            var userFrds = _context.FRDS.Where(f => f.UserId == userId).Include(c => c.User);

            if (User.IsInRole(RoleName.CanManageFRD))
                return View("List", frds);

            return View("ReadOnlyList", userFrds);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var frd = _context.FRDS.Include(f => f.User).Include(f => f.Channels).Include(f => f.TargetAudiences).SingleOrDefault(f => f.Id == id);

            if (frd == null)
            {
                return HttpNotFound();
            }

            var viewModel = new FRDDetailsViewModel
            {
                FRDS = frd,
                UnitApprovals = _context.UnitApprovals.Where(u => u.FRDId == id).Include(u => u.Department)
            };

            return View(viewModel);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReferenceNumber,Name,CreationDate,UserId,isApproved")] FRD fRD)
        {
            if (ModelState.IsValid)
            {
                fRD.CreationDate = DateTime.Now;
                fRD.isApproved = false;
                fRD.UserId = User.Identity.GetUserId();
                _context.FRDS.Add(fRD);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fRD);
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
                var userId = User.Identity.GetUserId();

                if (userId != model.UserId)
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

                var ManagerEmail = _context.Users.FirstOrDefault(u => u.Id == model.ManagerID).Email;

                var body = "<h3>Request For FRD Approval</h3><p>The FRD <strong>" + model.FrdName + "</strong> by <strong>" + model.DemandOwnerEmail + "</strong> is currently awaiting your approval</p><p><a href='https://localhost:44391/frd/confirm/" + model.FrdId + "'>View FRD</a></p>";
                var message = new MailMessage();  
                message.To.Add(new MailAddress(ManagerEmail));

                message.From = new MailAddress("cmpe406gradproject@outlook.com");  
  
                message.Subject = "FRD Approval";
                message.Body = string.Format(body, model.FrdId, model.DemandOwnerEmail, model.FrdName, model.ManagerID);
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
        public async Task<ActionResult> Confirm([Bind(Include = "id,Name,ReferenceNumber,CreationDate,UserId,isApproved")] FRD frd)
        {
            
            if (ModelState.IsValid)
            {
                _context.Entry(frd).State = EntityState.Modified;
                _context.SaveChanges();

                var units = _context.Departments.Where(d => d.CanApproveFRD == true);


                foreach (var unit in units)
                {
                    UnitApproval newUnit = new UnitApproval();

                    newUnit.DepartmentId = unit.Id;
                    newUnit.FRDId = frd.Id;

                    _context.UnitApprovals.Add(newUnit);
                }

                _context.SaveChanges();

                var distributionList = _context.DistributionLists.Include(d => d.User).ToList();


                foreach (var member in distributionList)
                {
                    var memberEmail = member.User.Email;

                    var body = "<h3>Request For FRD Approval</h3><p>The FRD <strong> " + frd.Name + "</strong> is currently awaiting your approval</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(memberEmail));

                    message.From = new MailAddress("cmpe406gradproject@outlook.com");

                    message.Subject = "FRD Approval";
                    message.Body = string.Format(body);
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
                    }
                }

                return RedirectToAction("Index");
            }

            return View(frd);
        }

    }
}