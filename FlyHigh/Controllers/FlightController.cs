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
    public class FlightController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /Flight/

        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.FromAirport).Include(f => f.ToAirport);
            return View(flights.ToList());
        }

        //
        // GET: /Flight/Details/5

        public ActionResult Details(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //
        // GET: /Flight/Create

        public ActionResult Create()
        {
            ViewBag.FromAirportId = new SelectList(db.Airports, "AirportId", "AirportCode");
            ViewBag.ToAirportId = new SelectList(db.Airports, "AirportId", "AirportCode");
            return View();
        }

        //
        // POST: /Flight/Create

        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.FromAirportId);
            ViewBag.ToAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.ToAirportId);
            return View(flight);
        }

        //
        // GET: /Flight/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }

            SelectList from = new SelectList(db.Airports, "AirportId", "AirportCode", flight.FromAirportId);
            SelectList to = new SelectList(db.Airports, "AirportId", "AirportCode", flight.ToAirportId);

            ViewBag.FromAirportId = from;
            ViewBag.ToAirportId = to;

            return View(flight);
        }

        //
        // POST: /Flight/Edit/5

        [HttpPost]
        public ActionResult Edit(Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.FromAirportId);
            ViewBag.ToAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.ToAirportId);
            return View(flight);
        }

        //
        // GET: /Flight/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //
        // POST: /Flight/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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