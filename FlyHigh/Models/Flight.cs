//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlyHigh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Flight
    {
        public Flight()
        {
            this.Schedules = new HashSet<Schedule>();
        }

        public int FlightId { get; set; }
        public int FromAirportId { get; set; }
        public int ToAirportId { get; set; }
        public System.TimeSpan Departure { get; set; }
        public int Duration { get; set; }
        public decimal BasePrice { get; set; }

        [Display(Name = "From Airport")]
        public virtual Airport FromAirport { get; set; }

        [Display(Name = "To Airport")]
        public virtual Airport ToAirport { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
