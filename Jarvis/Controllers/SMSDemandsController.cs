using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jarvis.Models;
using Microsoft.AspNet.Identity;

namespace Jarvis.Controllers
{
    public class SMSDemandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMSDemands
        public ActionResult Index()
        {
            var sMSDemands = db.SMSDemands.Include(s => s.SMSCode).Include(s => s.User);
            return View(sMSDemands.ToList());
        }

        // GET: SMSDemands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemand sMSDemand = db.SMSDemands.Find(id);
            if (sMSDemand == null)
            {
                return HttpNotFound();
            }
            return View(sMSDemand);
        }

        // GET: SMSDemands/Create
        public ActionResult Create()
        {
            ViewBag.SMSCodeId = new SelectList(db.SMSCodes, "Id", "CodeDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Sender,Content_tr,Content_en,SMSCodeId")] SMSDemand sMSDemand)
        {
            if (ModelState.IsValid)
            {
                sMSDemand.UpdateDate = DateTime.Now;
                sMSDemand.UserId = User.Identity.GetUserId();
                db.SMSDemands.Add(sMSDemand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SMSCodeId = new SelectList(db.SMSCodes, "Id", "CodeDescription", sMSDemand.SMSCodeId);
            return View(sMSDemand);
        }

        // GET: SMSDemands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemand sMSDemand = db.SMSDemands.Find(id);
            if (sMSDemand == null)
            {
                return HttpNotFound();
            }
            ViewBag.SMSCodeId = new SelectList(db.SMSCodes, "Id", "CodeDescription", sMSDemand.SMSCodeId);
            return View(sMSDemand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Sender,Content_tr,Content_en,SMSCodeId")] SMSDemand sMSDemand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMSDemand).State = EntityState.Modified;
                sMSDemand.UpdateDate = DateTime.Now;
                sMSDemand.UserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SMSCodeId = new SelectList(db.SMSCodes, "Id", "CodeDescription", sMSDemand.SMSCodeId);
            return View(sMSDemand);
        }

        // GET: SMSDemands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemand sMSDemand = db.SMSDemands.Find(id);
            if (sMSDemand == null)
            {
                return HttpNotFound();
            }
            return View(sMSDemand);
        }

        // POST: SMSDemands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMSDemand sMSDemand = db.SMSDemands.Find(id);
            db.SMSDemands.Remove(sMSDemand);
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
