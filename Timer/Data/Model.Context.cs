﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SprintEntities : DbContext
    {
        public SprintEntities()
            : base("name=SprintEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Racer> Racers { get; set; }
        public DbSet<RacesOption> RacesOptions { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ApplicationState> ApplicationStates { get; set; }
        public DbSet<RacersAtTheTrack> RacersAtTheTracks { get; set; }
        public DbSet<UserDataAboutCircle> UserDataAboutCircles { get; set; }
        public DbSet<RaceState> RaceStates { get; set; }
        public DbSet<RacerRaceState> RacerRaceStates { get; set; }
    }
}
