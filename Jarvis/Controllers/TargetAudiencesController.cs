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
    public class TargetAudiencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TargetAudiences
        public ActionResult Index()
        {
            return View(db.TargetAudiences.ToList());
        }

        // GET: TargetAudiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // GET: TargetAudiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TargetAudiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TargetAudience targetAudience)
        {
            if (ModelState.IsValid)
            {
                db.TargetAudiences.Add(targetAudience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetAudience);
        }

        // GET: TargetAudiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // POST: TargetAudiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TargetAudience targetAudience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetAudience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(targetAudience);
        }

        // GET: TargetAudiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // POST: TargetAudiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetAudience targetAudience = db.TargetAudiences.Find(id);

            var mappings = targetAudience.FRDAudienceMappings.Where(fam => fam.TargetAudienceId == id);
            foreach (var mapping in mappings)
            {

                var mappingsToUpdate = db.FRDAudienceMappings.Where(fam => fam.FRDId ==
                mapping.FRDId);

                foreach (var mappingToUpdate in mappingsToUpdate)
                {
                    if (mappingToUpdate.AudienceNumber > mapping.AudienceNumber)
                    {
                        mappingToUpdate.AudienceNumber--;
                    }
                }
            }

            db.TargetAudiences.Remove(targetAudience);
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
