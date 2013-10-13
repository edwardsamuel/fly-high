using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyHigh.Models
{
    public class ParkingModel
    {
        public ParkingModel(Schedule arrival, Schedule departure)
        {
            Arrival = arrival;
            Departure = departure;
        }

        public Schedule Arrival
        {
            get;
            set;
        }

        public Schedule Departure
        {
            get;
            set;
        }

        public TimeSpan Parking
        {
            get
            {
                if (Departure != null && Arrival != null)
                {
                    return Departure.DepartureTime - Arrival.ArrivalTime;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }
    }
}