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
    
    public partial class Comment
    {
        public long CommentID { get; set; }
        public Nullable<long> AnswerID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string Comment1 { get; set; }
        public string ImageURL { get; set; }
        public Nullable<long> ReplyTo { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    
        public virtual Answer Answer { get; set; }
    }
}
