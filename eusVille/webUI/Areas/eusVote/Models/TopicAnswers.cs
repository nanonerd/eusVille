using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webUI.Areas.eusVote.Models
{
    public class TopicAnswers
    {
        public long UserID { get; set; }
        public long AnswerID { get; set; }
        public decimal RatingScore { get; set; }
        public string Detail { get; set; }
        public int RatingCount { get; set; }
        public int CommentCount { get; set; }         
    }
}