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
    
    public partial class RacersAtTheTrack
    {
        public System.Guid Id { get; set; }
        public System.Guid FK_Racer { get; set; }
    
        public virtual Racer Racer { get; set; }
    }
}
