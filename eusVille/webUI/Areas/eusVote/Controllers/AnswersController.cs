using domain.eusVoteVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webUI.Common;
using webUI.Models;

namespace webUI.Areas.eusVote.Controllers
{
    [RouteArea("eusVote")]
    [RoutePrefix("Answers")]
    [Route("{action}/{id}")]
    public class AnswersController : Controller
    {
        private eusVoteEntities2 entVote = new eusVoteEntities2();

        // GET: eusVote/Answers
        [Route]
        public ActionResult Index()
        {
            var answers = entVote.Answers.Include(a => a.Topic);
            return View(answers.ToList());
        }

        // GET: eusVote/Answers/Details/5
        public ActionResult Details(long id)
        {
            Answer answer = entVote.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: eusVote/Answers/Create
        [Route("Create")]
        public ActionResult Create(int id)
        {
            Topic topic = entVote.Topics.Find(id);

            ViewBag.TopicTitle = topic.Topic1;
            ViewBag.TopicID = id.ToString();

            //ViewBag.TopicID = new SelectList(entVote.Topics, "TopicID", "Topic1");

            // If user is logged in, get UserID and set it to ViewBag to pass to the View.
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                Identity ident = new Identity();
                var userID = ident.GetUserID(userName);

                ViewBag.UserID = userID.ToString();
            }
            else
            {
                ViewBag.UserID = "0";
            }

            return View();
        }

        // POST: eusVote/Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="AnswerID,TopicID,Title,Detail,RatingScore,Count,UserID,TimeStamp")] Answer answer)
        public ActionResult Create([Bind(Include = "Firstname, Lastname")] TestPerson person)
        {
            if (ModelState.IsValid)
            {
                var firstname = person.Firstname;
                var lastname = person.Lastname;
            }

            return View(person);


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        answer.Count = 1;  // is this just a filler? 
            //        answer.TimeStamp = DateTime.Now;

            //        // Save answer
            //        entVote.Answers.Add(answer);
            //        entVote.SaveChanges();

            //        // Get AnswerID from answer just saved
            //        var answerID = (from ans in entVote.Answers
            //                        where ans.UserID == answer.UserID
            //                        orderby ans.AnswerID descending
            //                        select ans.AnswerID).FirstOrDefault();

            //        // TODO - put in code to insert into Rating table ****                
            //        var rating = new Rating();
                    
            //        rating.AnswerID = Convert.ToInt32(answerID.ToString());
            //        rating.UserID = answer.UserID;
            //        rating.Rating1 = Convert.ToInt32(answer.RatingScore);

            //        entVote.Ratings.Add(rating);
            //        entVote.SaveChanges();

            //        // TODO - put in code to insert comment, if given. Need comment input area on view.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }


            //    return RedirectToAction("Index");
            //}


            ////ViewBag.TopicID = new SelectList(entVote.Topics, "TopicID", "Topic1", answer.TopicID);
            //return View(answer);
        }

        // GET: eusVote/Answers/Edit/5
        public ActionResult Edit(long id)
        {
            Answer answer = entVote.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(entVote.Topics, "TopicID", "Topic1", answer.TopicID);
            return View(answer);
        }

        // POST: eusVote/Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AnswerID,TopicID,Title,Detail,RatingScore,Count,UserID,TimeStamp")] Answer answer)
        {
            // Need to post the rating to the Rating table separately.

            if (ModelState.IsValid)
            {
                entVote.Entry(answer).State = EntityState.Modified;
                entVote.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(entVote.Topics, "TopicID", "Topic1", answer.TopicID);
            return View(answer);
        }

        // GET: eusVote/Answers/Delete/5
        public ActionResult Delete(long id)
        {
            Answer answer = entVote.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: eusVote/Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Answer answer = entVote.Answers.Find(id);
            entVote.Answers.Remove(answer);
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
    }
}
