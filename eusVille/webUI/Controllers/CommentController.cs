using domain.eusVoteVM;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webUI.Models;

namespace webUI.Controllers
{
    public class CommentController : Controller
    {
        public string About()
        {
            return "hello about";
        }


        // GET: Comment View
        public ActionResult Index(int id)   // id is answerID (e.g., 10 for Minato Sushi Restaurant)
        {
            // If user is logged in, get UserID and set it to ViewBag to pass to the View.
            if (User.Identity.IsAuthenticated)
            {
                using (eusCommonEntities entCommon = new eusCommonEntities())
                {
                    var userName = User.Identity.Name;

                    var userID = (from i in entCommon.AspNetUsers
                                  where i.UserName == User.Identity.Name
                                  select i.UserID).FirstOrDefault();

                    ViewBag.UserName = userName;
                    ViewBag.UserID = userID.ToString();
                }
            }
            else
            {
                ViewBag.UserID = "0";
            }

            using (eusVoteEntities entVote = new eusVoteEntities())
            {
                var answer = (from o in entVote.Answers
                                   where o.AnswerID == id
                                   select new {o.TopicID, o.Title}).FirstOrDefault();

                ViewBag.AnswerTitle = answer.Title;

                var topic = (from t in entVote.Topics
                             where t.TopicID == answer.TopicID
                             select new { t.Topic1, t.TopicURL }).FirstOrDefault();

                // Topic title and id to be passed into comments view to create ReturnURL
                ViewBag.TopicTitle = topic.Topic1;
                ViewBag.TopicTitleURL = topic.TopicURL;
                ViewBag.TopicID = answer.TopicID.ToString();
            }

            ViewBag.AnswerID = id.ToString();

            // Get base URL (e.g., http://www.eusVille.com)
            ViewBag.BaseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            IEnumerable<CommentModel> comments;

            using (eusCommonEntities entCommon = new eusCommonEntities())
            {
                using (eusVoteEntities entVote = new eusVoteEntities())
                {
                    var listUsers = new List<AspNetUser>(entCommon.AspNetUsers);
                    var listComments = new List<Comment>(entVote.Comments);

                    // Do a join to listUsers to get the UserName
                    comments = (from c in listComments
                                join u in listUsers on c.UserID equals u.UserID
                                where (c.AnswerID == id)
                                select new CommentModel
                                {
                                    Author = u.UserName,
                                    Text = c.Comment1,
                                    UserID = u.UserID
                                });
                }
            }

            return View(comments);
        }


        // return json data for all comments for an AnswerID (e.g., Minato Sushi is an answer under the Sushi topic)
        [HttpGet]
        public ActionResult Comments(int id)
        {
            IEnumerable<CommentModel> comments;

            using (eusCommonEntities entCommon = new eusCommonEntities())
            {
                using (eusVoteEntities entVote = new eusVoteEntities())
                {
                    var listUsers = new List<AspNetUser>(entCommon.AspNetUsers);
                    var listComments = new List<Comment>(entVote.Comments);

                    // Do a join to listUsers to get the UserName
                    comments = (from c in listComments
                                join u in listUsers on c.UserID equals u.UserID
                                where (c.AnswerID == id)
                                select new CommentModel
                                {
                                    Author = u.UserName,
                                    Text = c.Comment1,
                                    UserID = u.UserID
                                });
                }
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}