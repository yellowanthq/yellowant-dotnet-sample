using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    public class UserIntegrationsController : Controller
    {
        private UserIntegrationDbContext db = new UserIntegrationDbContext();

        // GET: UserIntegrations
        public ActionResult Index()
        {
            return View(db.UserIntegration.ToList());
        }

        // GET: UserIntegrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserIntegration userIntegration = db.UserIntegration.Find(id);
            if (userIntegration == null)
            {
                return HttpNotFound();
            }
            return View(userIntegration);
        }

        // GET: UserIntegrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserIntegrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,YellowantUserID,YellowantTeamSubdomain,IntegrationID,InvokeName,YellowantIntegrationToken")] UserIntegration userIntegration)
        {
            if (ModelState.IsValid)
            {
                db.UserIntegration.Add(userIntegration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userIntegration);
        }

        // GET: UserIntegrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserIntegration userIntegration = db.UserIntegration.Find(id);
            if (userIntegration == null)
            {
                return HttpNotFound();
            }
            return View(userIntegration);
        }

        // POST: UserIntegrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,YellowantUserID,YellowantTeamSubdomain,IntegrationID,InvokeName,YellowantIntegrationToken")] UserIntegration userIntegration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userIntegration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userIntegration);
        }

        // GET: UserIntegrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserIntegration userIntegration = db.UserIntegration.Find(id);
            if (userIntegration == null)
            {
                return HttpNotFound();
            }
            return View(userIntegration);
        }

        // POST: UserIntegrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserIntegration userIntegration = db.UserIntegration.Find(id);
            db.UserIntegration.Remove(userIntegration);
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
