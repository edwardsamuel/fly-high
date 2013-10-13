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
    public class ScheduleController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /Schedule/

        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Flight).Include(s => s.Plane);
            return View(schedules.ToList());
        }

        //
        // GET: /Schedule/Details/5

        public ActionResult Details(long id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // GET: /Schedule/Create

        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId");
            ViewBag.PlaneId = new SelectList(db.Planes, "PlaneId", "PlaneType");
            return View();
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", schedule.FlightId);
            ViewBag.PlaneId = new SelectList(db.Planes, "PlaneId", "PlaneType", schedule.PlaneId);
            return View(schedule);
        }

        //
        // GET: /Schedule/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", schedule.FlightId);
            ViewBag.PlaneId = new SelectList(db.Planes, "PlaneId", "PlaneType", schedule.PlaneId);
            return View(schedule);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", schedule.FlightId);
            ViewBag.PlaneId = new SelectList(db.Planes, "PlaneId", "PlaneType", schedule.PlaneId);
            return View(schedule);
        }

        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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