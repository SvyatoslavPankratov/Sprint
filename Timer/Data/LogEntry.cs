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
    
    public partial class LogEntry
    {
        public System.Guid Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    }
}
