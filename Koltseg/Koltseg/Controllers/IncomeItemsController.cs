using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Koltseg.DAL;
using Koltseg.Models;

namespace Koltseg.Controllers
{
    public class IncomeItemsController : Controller
    {
        private KoltsegContext db = new KoltsegContext();

        // GET: IncomeItems
        public ActionResult Index()
        {
            var incomeItems = db.IncomeItems.Include(i => i.Category);
            return View(incomeItems.ToList());
        }

        // GET: IncomeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeItem incomeItem = db.IncomeItems.Find(id);
            if (incomeItem == null)
            {
                return HttpNotFound();
            }
            return View(incomeItem);
        }

        // GET: IncomeItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categorys, "ID", "Name");
            return View();
        }

        // POST: IncomeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,LastValue")] IncomeItem incomeItem)
        {
            if (ModelState.IsValid)
            {
                db.IncomeItems.Add(incomeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categorys, "ID", "Name", incomeItem.CategoryID);
            return View(incomeItem);
        }

        // GET: IncomeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeItem incomeItem = db.IncomeItems.Find(id);
            if (incomeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categorys, "ID", "Name", incomeItem.CategoryID);
            return View(incomeItem);
        }

        // POST: IncomeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,LastValue")] IncomeItem incomeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incomeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categorys, "ID", "Name", incomeItem.CategoryID);
            return View(incomeItem);
        }

        // GET: IncomeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeItem incomeItem = db.IncomeItems.Find(id);
            if (incomeItem == null)
            {
                return HttpNotFound();
            }
            return View(incomeItem);
        }

        // POST: IncomeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncomeItem incomeItem = db.IncomeItems.Find(id);
            db.IncomeItems.Remove(incomeItem);
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
