using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SweetStack.DataAccess;
using SweetStack.DomainObjects;

namespace SweetStack.Controllers
{
    public class TestsController : Controller
    {
        private LogContext db = new LogContext();

        // GET: Tests
        public ActionResult Index()
        {
            return View(db.SweetTests.ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SweetTest sweetTest = db.SweetTests.Find(id);
            if (sweetTest == null)
            {
                return HttpNotFound();
            }
            return View(sweetTest);
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SweetTest sweetTest = db.SweetTests.Find(id);
            if (sweetTest == null)
            {
                return HttpNotFound();
            }
            return View(sweetTest);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Timestamp,Completed,SweetStackCode")] SweetTest sweetTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sweetTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sweetTest);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SweetTest sweetTest = db.SweetTests.Find(id);
            if (sweetTest == null)
            {
                return HttpNotFound();
            }
            return View(sweetTest);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SweetTest sweetTest = db.SweetTests.Find(id);
            db.SweetTests.Remove(sweetTest);
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
