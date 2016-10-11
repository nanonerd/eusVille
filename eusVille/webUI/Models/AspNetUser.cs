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
    
    public partial class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public long UserID { get; set; }
        public Nullable<long> evNum { get; set; }
        public Nullable<long> VilleNum { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public Nullable<int> BirthYear { get; set; }
        public Nullable<int> BirthMonth { get; set; }
        public Nullable<int> BirthDay { get; set; }
        public string ImageURL { get; set; }
        public string ThumbURL { get; set; }
        public string Ethnicity1 { get; set; }
        public string Ethnicity2 { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public Nullable<int> PostalCode { get; set; }
        public Nullable<bool> IsComplete { get; set; }
    }
}
