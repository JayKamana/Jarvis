using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jarvis.Models;

namespace Jarvis.Controllers
{
    public class SMSCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMSCodes
        public ActionResult Index()
        {
            return View(db.SMSCodes.ToList());
        }

        // GET: SMSCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSCode sMSCode = db.SMSCodes.Find(id);
            if (sMSCode == null)
            {
                return HttpNotFound();
            }
            return View(sMSCode);
        }

        // GET: SMSCodes/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReferenceCode,CodeDescription")] SMSCode sMSCode)
        {
            if (ModelState.IsValid)
            {
                db.SMSCodes.Add(sMSCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sMSCode);
        }

        // GET: SMSCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSCode sMSCode = db.SMSCodes.Find(id);
            if (sMSCode == null)
            {
                return HttpNotFound();
            }
            return View(sMSCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReferenceCode,CodeDescription")] SMSCode sMSCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMSCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sMSCode);
        }

        // GET: SMSCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSCode sMSCode = db.SMSCodes.Find(id);
            if (sMSCode == null)
            {
                return HttpNotFound();
            }
            return View(sMSCode);
        }

        // POST: SMSCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMSCode sMSCode = db.SMSCodes.Find(id);
            db.SMSCodes.Remove(sMSCode);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
