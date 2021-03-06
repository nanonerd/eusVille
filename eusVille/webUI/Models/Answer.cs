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
    
    public partial class Answer
    {
        public Answer()
        {
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<Rating>();
        }
    
        public long AnswerID { get; set; }
        public Nullable<long> TopicID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Nullable<decimal> RatingScore { get; set; }
        public Nullable<long> Count { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
