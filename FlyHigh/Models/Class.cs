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
    
    public partial class Class
    {
        public Class()
        {
            this.PlaneClasses = new HashSet<PlaneClass>();
            this.TicketInstances = new HashSet<TicketInstance>();
        }
    
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    
        public virtual ICollection<PlaneClass> PlaneClasses { get; set; }
        public virtual ICollection<TicketInstance> TicketInstances { get; set; }
    }
}
