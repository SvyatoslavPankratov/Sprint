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
    
    public partial class Racer
    {
        public Racer()
        {
            this.Results = new HashSet<Result>();
            this.ApplicationStates = new HashSet<ApplicationState>();
            this.RacersAtTheTracks = new HashSet<RacersAtTheTrack>();
            this.UserDataAboutCircles = new HashSet<UserDataAboutCircle>();
            this.RaceStates = new HashSet<RaceState>();
            this.Cars = new HashSet<Car>();
        }
    
        public System.Guid Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<ApplicationState> ApplicationStates { get; set; }
        public virtual ICollection<RacersAtTheTrack> RacersAtTheTracks { get; set; }
        public virtual ICollection<UserDataAboutCircle> UserDataAboutCircles { get; set; }
        public virtual ICollection<RaceState> RaceStates { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
