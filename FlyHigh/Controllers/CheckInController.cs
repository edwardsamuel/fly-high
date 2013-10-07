using FlyHigh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace FlyHigh.Controllers
{
    public class CheckInController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /CheckIn/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CheckIn/CheckIn/5

        //public ActionResult CheckIn(int id = 0)
        //{
        //    var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == id).FirstOrDefault();
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        //
        // POST: /CheckIn/CheckIn/5

        [HttpPost]
        public ActionResult Index(int id = 0, string lastName = "", string departure = "")
        {
            var airport = db.Airports.Where(ps => ps.AirportName == departure).FirstOrDefault();
            var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == id && ps.BookingLastName == lastName).FirstOrDefault();
            
            CheckInModel fm = new CheckInModel();
            fm.booking = booking;

            if (booking == null)
            {
                return HttpNotFound();
            }
            else if (airport != null && booking.Tickets.FirstOrDefault().TicketInstances.FirstOrDefault().Schedule.Flight.FromAirport.AirportId == airport.AirportId)
            {
                return View("CheckIn", fm);
            }
            else
            {
                return HttpNotFound();
            }
            
            //return RedirectToActionPermanent("CheckIn", booking);
        }

        [HttpPost]
        public ActionResult CheckIn(CheckInModel model)
        {
            if (model.DynamicMultiBoxes != null)
            {
                foreach (var i in model.DynamicMultiBoxes)
                {
                    var ticketToUpdate = db.Tickets.Where(ps => ps.TicketId == i).Single();
                    ticketToUpdate.CheckIn = DateTime.Now;
                    db.Entry(ticketToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    // Model.DynamicTableRows.FirstOrDefault(m=>m.ID == i).Name <br />
                }
            }

            //if (ModelState.IsValid)
            //    return View("FormResult", model);
            //else
                return View("Success");

            //foreach (var item in checkin)
            //{

            //    try
            //    {

            //        int a;

            //    }
            //    catch (Exception err)
            //    {

            //        ModelState.AddModelError("", "Failed On Id " + item.ToString() + " : " + err.Message);
            //        return View("CheckIn");
            //    }
            //}
            //return View("CheckIn");
        }
    }
}
