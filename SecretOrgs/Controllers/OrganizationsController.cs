﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecretOrgs.Models;

namespace SecretOrgs.Controllers
{
    [Authorize(Roles = "Intern")]
    public class OrganizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organizations
        public ActionResult Index()
        {
            return View(db.Organizations.ToList());
        }

        // GET: Organizations/Details/5
         [Authorize(Roles = "Grunt")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: Organizations/Create
        [Authorize(Roles="Big Boss")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Big Boss")]
        public ActionResult Create([Bind(Include = "Id,OrganizationName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Boss")]
        public ActionResult Edit([Bind(Include = "Id,OrganizationName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        [Authorize(Roles = "KeeperOfSecrets")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [Authorize(Roles = "KeeperOfSecrets")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
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
