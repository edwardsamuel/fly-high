using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyHigh.Models;

namespace FlyHigh.Controllers
{
    public class PilotController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /Pilot/

        public ActionResult Index()
        {
            return View(db.Pilots.ToList());
        }

        //
        // GET: /Pilot/Details/5

        public ActionResult Details(int id = 0)
        {
            Pilot pilot = db.Pilots.Find(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        //
        // GET: /Pilot/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pilot/Create

        [HttpPost]
        public ActionResult Create(Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                db.Pilots.Add(pilot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pilot);
        }

        //
        // GET: /Pilot/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pilot pilot = db.Pilots.Find(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        //
        // POST: /Pilot/Edit/5

        [HttpPost]
        public ActionResult Edit(Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pilot);
        }

        //
        // GET: /Pilot/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pilot pilot = db.Pilots.Find(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        //
        // POST: /Pilot/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pilot pilot = db.Pilots.Find(id);
            db.Pilots.Remove(pilot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}