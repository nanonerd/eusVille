using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.eusVoteVM
{
    public class CommentModel
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public long UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string returnURL { get; set; }
    }
}
