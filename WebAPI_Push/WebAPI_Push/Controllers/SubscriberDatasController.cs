using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPI_Push.Models;

namespace WebAPI_Push.Views
{
    [RoutePrefix("subscriberdatas")]
    public class SubscriberDatasController : Controller
    {
        private SubscriberModel db = new SubscriberModel();

        public SubscriberDatasController(SubscriberModel context)
        {
            db = context;
        }
        // GET: SubscriberDatas
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.SubscriberDatas.ToList());
        }

        // GET: SubscriberDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriberData subscriberData = db.SubscriberDatas.Find(id);
            if (subscriberData == null)
            {
                return HttpNotFound();
            }
            return View(subscriberData);
        }

        // GET: SubscriberDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubscriberDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PrivateKey,PublicKey,Header,Message,URL,Endpoint")] SubscriberData subscriberData)
        {
            if (ModelState.IsValid)
            {
                db.SubscriberDatas.Add(subscriberData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscriberData);
        }

        // GET: SubscriberDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriberData subscriberData = db.SubscriberDatas.Find(id);
            if (subscriberData == null)
            {
                return HttpNotFound();
            }
            return View(subscriberData);
        }

        // POST: SubscriberDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PrivateKey,PublicKey,Header,Message,URL,Endpoint")] SubscriberData subscriberData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriberData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriberData);
        }

        // GET: SubscriberDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriberData subscriberData = db.SubscriberDatas.Find(id);
            if (subscriberData == null)
            {
                return HttpNotFound();
            }
            return View(subscriberData);
        }

        // POST: SubscriberDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubscriberData subscriberData = db.SubscriberDatas.Find(id);
            db.SubscriberDatas.Remove(subscriberData);
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
