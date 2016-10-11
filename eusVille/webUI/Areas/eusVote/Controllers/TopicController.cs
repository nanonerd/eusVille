using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using webUI.Areas.eusVote.Models;
using webUI.Common;
using webUI.Controllers;
using webUI.Models;

namespace webUI.Areas.eusVote.Controllers
{   
    [RouteArea("eusVote")]
    [RoutePrefix("Topic")]
    [Route("{action}/{id}")]
    public class TopicController : Controller
    {
        private eusCommonEntities entCommon = new eusCommonEntities();
        private eusVoteEntities2 entVote = new eusVoteEntities2();

        // GET: eusVote/Topic. All topics. 
        [Route]
        public ActionResult Index()
        {
            var topics = entVote.Topics.Include(t => t.CategoryList);
            return View(topics.ToList());
        }

        public ActionResult TopicAnswers(int id)
        {
            // Get topic title for id.
            var topicTitle = (from t in entVote.Topics
                              where t.TopicID == id
                              select t.Topic1).FirstOrDefault();

            ViewBag.TopicTitle = topicTitle;
            ViewBag.TopicID = id;
           
            // Get base URL (e.g., http://www.eusVille.com)
            ViewBag.BaseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                            


            // Get top 8 answers for topic based on TopicID.
                        
            // listAnswers = (from a in entVote.Answers                           
            //               where a.TopicID == id
            //               orderby a.RatingScore descending                          
            //               select new {a.UserID, a.AnswerID, a.Detail, a.RatingScore}).Take(8);
                                    
            //var answers = (from a in entVote.Answers                           
            //               where a.TopicID == id
            //               orderby a.RatingScore descending                          
            //               select a).Take(8);
                        
            //ViewBag.Count = answers.Count();

            // If user is logged in, get UserID and set it to ViewBag to pass to the View.
            var userID = 0;

            if (User.Identity.IsAuthenticated)
            {                              
                var userName = User.Identity.Name;
                Identity ident = new Identity();
                userID = (int)ident.GetUserID(userName);                
            }

            ViewBag.UserID = userID.ToString();

            //else
            //{
            //    ViewBag.UserID = "0";
            //}

            //List<webUI.Models.GetTopicAnswers_Result> topicAnswersList = new List<webUI.Models.GetTopicAnswers_Result>();

            var answers = entVote.GetTopicAnswers(id, userID).ToList();                        

            //topicAnswersList.Add(answers);

            return View(answers);
        }

        // Create child action to render comments
        public PartialViewResult Comments(int answerID)
        {
            using (eusVoteEntities ent = new eusVoteEntities())
            {
                var comments = (from c in entVote.Comments
                                where c.AnswerID == answerID
                                orderby c.TimeStamp descending
                                select c).ToList();

                // This is the URL used in the partial view to go to page that displays all comments for answerID
                ViewBag.Url = "/comments/" + answerID.ToString();
                return PartialView(comments);
            }
        }


        // GET: eusVote/Topic/Details/5
        public ActionResult Details(long id)
        {
            Topic topic = entVote.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: eusVote/Topic/Create
        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(entVote.CategoryLists, "CategoryID", "Category");
            return View();
        }

        // POST: eusVote/Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TopicID,Topic1,TopicURL,Search,Tags,CategoryID,UserId,TimeStamp")] Topic topic)
        {
            if (ModelState.IsValid)
            {                
                // If user is logged in, get UserID. To get to this point, user must be logged in.
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.Identity.Name;
                    Identity ident = new Identity();           

                    topic.UserId = ident.GetUserID(userName);
                }
                else
                {
                    topic.UserId = 0;
                }

                SearchTools st = new SearchTools();

                topic.TopicURL = st.CreateSearchURL(topic.Topic1);               
                topic.TimeStamp = DateTime.Now;

                entVote.Topics.Add(topic);
                entVote.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(entVote.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // GET: eusVote/Topic/Edit/5
        public ActionResult Edit(long id)
        {
            Topic topic = entVote.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(entVote.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // POST: eusVote/Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TopicID,Topic1,TopicURL,Search,Tags,CategoryID,UserId,TimeStamp")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                entVote.Entry(topic).State = EntityState.Modified;
                entVote.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(entVote.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // GET: eusVote/Topic/Delete/5
        public ActionResult Delete(long id)
        {
            Topic topic = entVote.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: eusVote/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Topic topic = entVote.Topics.Find(id);
            entVote.Topics.Remove(topic);
            entVote.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entVote.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> ////////////////////////////////////////////////////////////////////////
        /// GETS    GETS    GETS    GETS    GETS    GETS    GETS    GETS     
        /// </summary> //////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        public string TopicGet(string Filter, string FilterData)
        {
            string jsonReturn = "";

            try
            {
                switch (Filter)
                {
                    case "AnswersGet":
                        {
                            int topicID = Convert.ToInt32(FilterData);

                            // Get topic title for id.
                            var topicTitle = (from t in entVote.Topics
                                              where t.TopicID == topicID
                                              select t.Topic1).FirstOrDefault();

                            ViewBag.TopicTitle = topicTitle;
                            ViewBag.TopicID = topicID;

                            // Get top 8 answers for topic based on TopicID.
                            //var answers = (from o in entVote.Answers
                            //               where o.TopicID == topicID
                            //               orderby o.RatingScore descending
                            //               select o).Take(8);

                            // If user is logged in, get UserID and set it to ViewBag to pass to the View.
                            var userID = 0;

                            if (User.Identity.IsAuthenticated)
                            {
                                var userName = User.Identity.Name;
                                Identity ident = new Identity();
                                userID = (int)ident.GetUserID(userName);
                                ViewBag.UserID = userID.ToString();
                            }
                            else
                            {
                                ViewBag.UserID = "0";
                            }                         

                            var answers = entVote.GetTopicAnswers(topicID, userID).ToList();

                            ViewBag.Count = answers.Count();                                                      

                            jsonReturn = JsonConvert.SerializeObject(answers, Formatting.Indented,
                                                new JsonSerializerSettings
                                                {
                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                });


                            break;
                        }
                    case "CommentsGet":
                        {
                            int answerID = Convert.ToInt32(FilterData);

                            using (eusVoteEntities entVote = new eusVoteEntities())
                            {
                                var comments = (from c in entVote.Comments
                                                where c.AnswerID == answerID
                                                select c).Take(5).ToArray();

                                jsonReturn = JsonConvert.SerializeObject(comments, Formatting.Indented,
                                                new JsonSerializerSettings
                                                {
                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                });
                            }

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                var seriesContent = new Dictionary<string, string>
                {
                    {"user" , "test"}, {"detail", ex.ToString()}, {"action", Filter},
                    {"errorLocation", "eusVote > TopicController > Topic > Get > " + Filter} , {"level", "high"}
                };

                jsonReturn = JsonConvert.SerializeObject(seriesContent);

                // Log error
                CommonController cont = new CommonController();
                cont.Common("ErrorHandle", jsonReturn);

                return jsonReturn;
            }

            return jsonReturn;
        }


        /// <summary> ////////////////////////////////////////////////////////////////////////
        /// POSTS   POSTS   POSTS   POSTS   POSTS   POSTS   POSTS   POSTS   
        /// </summary> ////////////////////////////////////////////////////////////////////////       

        [HttpPost]
        public string Topic(string Filter, string FilterData)
        {
            string jsonReturn = "";
            string userName = "";

            // If user is logged in, get UserID. To get to this point, user must be logged in.
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;                
            }
            else
            {
                throw new Exception("Must be logged in to post.");
            }

            //FilterData = null;   // to test errorHandle
            try
            {
                switch (Filter)
                {
                    case "SetRating":
                        {
                            // Parse out JSON data
                            JObject o = JObject.Parse(FilterData);

                            int userID = (int)o.SelectToken("UserID");                            
                            int answerID = (int)o.SelectToken("AnswerID");
                            int rating = (int)o.SelectToken("Rating");
                            string comment = (string)o.SelectToken("Comment");
                            
                            using (eusVoteEntities entVote = new eusVoteEntities())
                            {
                                // Save comment only if not empty.                               
                                if (!string.IsNullOrWhiteSpace(comment))
                                {
                                    Comment com = new Comment();
                                    com.AnswerID = answerID;
                                    com.UserID = userID;
                                    com.Comment1 = comment;
                                    com.TimeStamp = DateTime.Now;

                                    entVote.Comments.Add(com);
                                    entVote.SaveChanges();
                                }

                                // Logic to set rating
                                var record = (from e in entVote.Ratings
                                              where e.UserID == userID
                                                 && e.AnswerID == answerID
                                              select e).FirstOrDefault();

                                // Set the rating for an answer per this user
                                if (record != null)  // If exist, then Update rating
                                {
                                    record.Rating1 = rating;
                                    entVote.SaveChanges();
                                }
                                else   // If not exist, then Insert new rating per this user
                                {
                                    Rating r = new Rating();
                                    r.UserID = userID;
                                    r.AnswerID = answerID;
                                    r.Rating1 = rating;

                                    entVote.Ratings.Add(r);
                                    entVote.SaveChanges();
                                }

                                //////////////////////////////////////////////////////////////////////////
                                //////// Update the RatingScore for the Answer/Answer in the Rating table
                                //////////////////////////////////////////////////////////////////////////

                                // Get all the rating values for the answer (will be summed up in RatingScore calc below)
                                var totalRatings = (from t in entVote.Ratings
                                                    where t.AnswerID == answerID
                                                    select t.Rating1);

                                // Get the answer info for the answer that is being updated
                                var answer = (from op in entVote.Answers
                                              where op.AnswerID == answerID
                                              select op).FirstOrDefault();

                                // If record is null, then Insert was done so increment count.
                                if (record == null)
                                {
                                    if (answer.Count == null)
                                    {
                                        answer.Count = 1;
                                    }
                                    else
                                    {
                                        answer.Count++;
                                    }
                                }

                                // Calcuate new avg rating based on latest numbers
                                answer.RatingScore = Math.Round(((decimal)totalRatings.Sum() / (decimal)totalRatings.Count()), 2);
                                entVote.SaveChanges();

                                // Create dictionary of values to return
                                var seriesContent = new Dictionary<string, string>
                                {
                                    {"error", "false"} , {"count", answer.Count.ToString()}, {"score", answer.RatingScore.ToString()}
                                };

                                // Serialize the dictionary to return as json
                                jsonReturn = JsonConvert.SerializeObject(seriesContent);
                                break;
                            }
                        }

                    case "CommentPost":
                        {
                            // Parse out JSON data
                            JObject o = JObject.Parse(FilterData);
                                                                                    
                            int userID = (int)o.SelectToken("UserID");                            
                            int answerID = (int)o.SelectToken("AnswerID");
                            string comment = (string)o.SelectToken("Comment");
                            DateTime stamp = DateTime.Now;

                            using (eusVoteEntities entVote = new eusVoteEntities())
                            {
                                Comment com = new Comment();
                                com.AnswerID = answerID;
                                com.UserID = userID;
                                com.Comment1 = comment;
                                com.TimeStamp = stamp;

                                entVote.Comments.Add(com);
                                entVote.SaveChanges();
                            }
                            break;
                        }

                    default:
                        {
                            var seriesContent = new Dictionary<string, string>
                                    {
                                        {"error", "true"} , {"message", "Controller: eusVote > TopicController > Topic [POST] doesn't have matching filter for filter = " + Filter }
                                    };

                            jsonReturn = JsonConvert.SerializeObject(seriesContent);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                // Create error data to serialize to JSON.
                var seriesContent = new Dictionary<string, string>
                {
                    {"user" , userName}, {"detail", ex.Message}, {"action", Filter},
                    {"errorLocation", "eusVote > TopicController > Topic [POST] " + Filter} , {"level", "high"}
                };

                // Serialize error data.
                jsonReturn = JsonConvert.SerializeObject(seriesContent);

                // Log error
                CommonController cont = new CommonController();
                cont.Common("ErrorHandle", jsonReturn);

                return jsonReturn;
            }

            return jsonReturn;
        }

    }
}
