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
    
    public partial class SprintEntities1 : DbContext
    {
        public SprintEntities1()
            : base("name=SprintEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Result> Results { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<RacesOption> RacesOptions { get; set; }
        public DbSet<Racer> Racers { get; set; }
    }
}
