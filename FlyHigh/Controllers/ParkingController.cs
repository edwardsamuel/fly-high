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
    public class ParkingController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();
        //
        // GET: /Parking/

        //public ActionResult Index()
        //{
        //    var flights = db.Flights.Include(f => f.FromAirport).Include(f => f.ToAirport);
        //    List<TimeSpan> arrivalTimeList = new List<TimeSpan>();
        //    List<ParkingItem> ParkingViewModel = new List<ParkingItem>();
        //    foreach (var f in flights)
        //    {
        //        TimeSpan arrivalTime = TimeSpan.FromMinutes(f.Duration) + f.Departure;
        //        ParkingItem p = new ParkingItem();
        //        p.Departure = f.Departure;
        //        p.Duration = f.Duration;
        //        p.FlightId = f.FlightId;
        //        p.FromAirport = f.FromAirport;
        //        p.FromAirportID = f.FromAirportId;
        //        p.ToAirport = f.ToAirport;
        //        p.ToAirportID = f.ToAirportId;
        //        p.Arrival = arrivalTime;
        //        p.ParkingDuration = Convert.ToInt32((p.Arrival - p.Departure).TotalHours);
        //        ParkingViewModel.Add(p);
        //    }
        //    //ViewData["arrivalTimeList"] = new SelectList(arrivalTimeList);
        //    //return View(flights.ToList());
        //    return View(ParkingViewModel);
        //}

        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Parking/Details/5

        public ActionResult Details(int id)
        {
            var schedules = db.Schedules.Include(s => s.Flight).Include(s => s.Plane);
            return View(schedules.ToList());
        }

        //
        // GET: /Parking/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Parking/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Parking/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Parking/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Parking/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Parking/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
