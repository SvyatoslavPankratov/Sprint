//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sprint.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarClass
    {
        public CarClass()
        {
            this.Cars = new HashSet<Car>();
            this.RacesOptions = new HashSet<RacesOption>();
            this.ApplicationStates = new HashSet<ApplicationState>();
            this.RaceStates = new HashSet<RaceState>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<RacesOption> RacesOptions { get; set; }
        public virtual ICollection<ApplicationState> ApplicationStates { get; set; }
        public virtual ICollection<RaceState> RaceStates { get; set; }
    }
}
