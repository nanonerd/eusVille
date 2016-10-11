using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webUI.Models;

namespace webUI.Controllers.Admin
{
    public class Admin_CategoryListController : Controller
    {
        private eusVoteEntities db = new eusVoteEntities();

        // GET: Admin_CategoryList
        public ActionResult Index()
        {
            return View(db.CategoryLists.ToList());
        }

        // GET: Admin_CategoryList/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // GET: Admin_CategoryList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_CategoryList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Category")] CategoryList categoryList)
        {
            if (ModelState.IsValid)
            {
                db.CategoryLists.Add(categoryList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryList);
        }

        // GET: Admin_CategoryList/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // POST: Admin_CategoryList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Category")] CategoryList categoryList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryList);
        }

        // GET: Admin_CategoryList/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // POST: Admin_CategoryList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CategoryList categoryList = db.CategoryLists.Find(id);
            db.CategoryLists.Remove(categoryList);
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
