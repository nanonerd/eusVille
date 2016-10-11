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
    public class HomeController : Controller
    {
        private eusVoteEntities db = new eusVoteEntities();

        // GET: eusVote/Home - Get a list of trending topics
        public ActionResult Index()
        {
            // If user is logged in, get UserID and set it to ViewBag to pass to the View.
            var userID = 0;

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                Identity ident = new Identity();
                userID = (int)ident.GetUserID(userName);
            }

            ViewBag.UserID = userID.ToString();
            
            var topics = db.Topics.Include(t => t.CategoryList);
            return View(topics.ToList());
        }

        // GET: eusVote/Home/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: eusVote/Home/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.CategoryLists, "CategoryID", "Category");
            return View();
        }

        // POST: eusVote/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,Topic1,Tags,CategoryID,UserId,TimeStamp,TopicURL,Search")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // GET: eusVote/Home/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // POST: eusVote/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,Topic1,Tags,CategoryID,UserId,TimeStamp,TopicURL,Search")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryLists, "CategoryID", "Category", topic.CategoryID);
            return View(topic);
        }

        // GET: eusVote/Home/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: eusVote/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
