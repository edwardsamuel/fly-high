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
        private static double BAGGAGE_CHARGE_MULTIPLIER = 50000;
        private static double MAX_WEIGHT_PER_PERSON = 20;

        private ErlanggaEntities db = new ErlanggaEntities();

        [HttpPost]
        public ActionResult Index(int id = 0)
        {
            var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == id).FirstOrDefault();

            CheckInModel fm = new CheckInModel();
            fm.booking = booking;

            if (booking == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("CheckIn", fm);
            }
        }

        //
        // GET: /CheckIn/

        public ActionResult Index()
        {
            return View();
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
                }
            }
            var tickets = db.Tickets.Include(ps => ps.Booking).Where(ps => ps.BookingId == model.booking.BookingId && ps.CheckIn != null).ToList();
            //model.booking.Tickets = (ICollection<Ticket>)tickets;
            return View("Baggage", tickets);
        }

        //
        // GET: /CheckIn/AddBaggage/5

        public ActionResult AddBaggage(int id = 0)
        {
            Baggage baggage = new Baggage() { TicketId = id };
            return View("AddBaggage", baggage);
        }

        //
        // POST: /CheckIn/AddBaggage/5

        [HttpPost]
        public ActionResult AddBaggage(Baggage baggageToAdd)
        {
            if (ModelState.IsValid)
            {
                db.Baggages.Add(baggageToAdd);
                db.SaveChanges();

                double totalWeight = 0;
                var baggages = db.Baggages.Where(ps => ps.TicketId == baggageToAdd.TicketId).ToList();
                foreach (var baggage in baggages)
                {
                    totalWeight += baggage.Weight;
                }
                if (totalWeight > MAX_WEIGHT_PER_PERSON)
                {
                    var ticketToUpdate = db.Tickets.Where(ps => ps.TicketId == baggageToAdd.TicketId).Single();
                    ticketToUpdate.BaggageCharge = (decimal)((totalWeight - MAX_WEIGHT_PER_PERSON) * BAGGAGE_CHARGE_MULTIPLIER);
                    db.Entry(ticketToUpdate).State = EntityState.Modified;
                }

                db.SaveChanges();


                var bookingId = db.Tickets.Where(ps => ps.TicketId == baggageToAdd.TicketId).FirstOrDefault().BookingId;

                var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == bookingId).FirstOrDefault();

                //var tickets = db.Tickets.Include(ps => ps.Booking).Where(ps => ps.BookingId == baggageToAdd.Ticket.BookingId && ps.CheckIn != null).ToList()
                //var tickets2 = db.Tickets.Include(ps => ps.Booking);
                //var tickets3 = tickets2.Where(ps => ps.BookingId == baggageToAdd.Ticket.BookingId && ps.CheckIn != null).ToList();
                //var tickets = tickets3.ToList();

                return View("Baggage", booking.Tickets.ToList());
            }
            //ViewBag.FromAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.FromAirportId);
            //ViewBag.ToAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.ToAirportId);
            return View();
        }

        [HttpPost]
        public ActionResult Success(Baggage baggageToAdd)
        {
            return View("Success");
        }
    }
}
