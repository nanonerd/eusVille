//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin_Log
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsError { get; set; }
    }
}