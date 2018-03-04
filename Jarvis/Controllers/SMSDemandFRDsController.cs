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
    public class SMSDemandFRDsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMSDemandFRDs
        public ActionResult Index()
        {
            var sMSDemandFRDs = db.SMSDemandFRDs.Include(s => s.FRD).Include(s => s.SMSDemand);
            return View(sMSDemandFRDs.ToList());
        }

        // GET: SMSDemandFRDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemandFRD sMSDemandFRD = db.SMSDemandFRDs.Find(id);
            if (sMSDemandFRD == null)
            {
                return HttpNotFound();
            }
            return View(sMSDemandFRD);
        }

        // GET: SMSDemandFRDs/Create
        public ActionResult Create()
        {
            ViewBag.FRDId = new SelectList(db.FRDS, "Id", "Name");
            ViewBag.SMSDemandId = new SelectList(db.SMSDemands, "ID", "Sender");
            return View();
        }

        // POST: SMSDemandFRDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FRDId,SMSDemandId")] SMSDemandFRD sMSDemandFRD)
        {
            if (ModelState.IsValid)
            {
                db.SMSDemandFRDs.Add(sMSDemandFRD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FRDId = new SelectList(db.FRDS, "Id", "Name", sMSDemandFRD.FRDId);
            ViewBag.SMSDemandId = new SelectList(db.SMSDemands, "ID", "Sender", sMSDemandFRD.SMSDemandId);
            return View(sMSDemandFRD);
        }

        // GET: SMSDemandFRDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemandFRD sMSDemandFRD = db.SMSDemandFRDs.Find(id);
            if (sMSDemandFRD == null)
            {
                return HttpNotFound();
            }
            ViewBag.FRDId = new SelectList(db.FRDS, "Id", "Name", sMSDemandFRD.FRDId);
            ViewBag.SMSDemandId = new SelectList(db.SMSDemands, "ID", "Sender", sMSDemandFRD.SMSDemandId);
            return View(sMSDemandFRD);
        }

        // POST: SMSDemandFRDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FRDId,SMSDemandId")] SMSDemandFRD sMSDemandFRD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMSDemandFRD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FRDId = new SelectList(db.FRDS, "Id", "Name", sMSDemandFRD.FRDId);
            ViewBag.SMSDemandId = new SelectList(db.SMSDemands, "ID", "Sender", sMSDemandFRD.SMSDemandId);
            return View(sMSDemandFRD);
        }

        // GET: SMSDemandFRDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSDemandFRD sMSDemandFRD = db.SMSDemandFRDs.Find(id);
            if (sMSDemandFRD == null)
            {
                return HttpNotFound();
            }
            return View(sMSDemandFRD);
        }

        // POST: SMSDemandFRDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMSDemandFRD sMSDemandFRD = db.SMSDemandFRDs.Find(id);
            db.SMSDemandFRDs.Remove(sMSDemandFRD);
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
