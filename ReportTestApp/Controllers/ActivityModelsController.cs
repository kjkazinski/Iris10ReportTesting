using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IrisWeb.Code.Data.Models.Database;
using ReportTestApp.Models;

namespace ReportTestApp.Controllers
{
    public class ActivityModelsController : Controller
    {
        private readonly ReportTestAppContext db = new ReportTestAppContext();

        // GET: ActivityModels
        public ActionResult Index()
        {
            return View(db.ActivityModels.ToList());
        }

        // GET: ActivityModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.ActivityModels.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            return View(activityModel);
        }

        // GET: ActivityModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Activity_Key,Name,Description,NameDesc,DescName,Perform_Standard,Work_Unit,WorkComp_Key,UOM_Key,Work_Methods,Inspection,Authorize,Active,User1,User2,User3,User4,User5,User6,User7,User8,User9,User10,CreateDate,DateStamp,SecurityUser_Key")] ActivityModel activityModel)
        {
            if (ModelState.IsValid)
            {
                db.ActivityModels.Add(activityModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityModel);
        }

        // GET: ActivityModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.ActivityModels.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            return View(activityModel);
        }

        // POST: ActivityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Activity_Key,Name,Description,NameDesc,DescName,Perform_Standard,Work_Unit,WorkComp_Key,UOM_Key,Work_Methods,Inspection,Authorize,Active,User1,User2,User3,User4,User5,User6,User7,User8,User9,User10,CreateDate,DateStamp,SecurityUser_Key")] ActivityModel activityModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityModel);
        }

        // GET: ActivityModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModel activityModel = db.ActivityModels.Find(id);
            if (activityModel == null)
            {
                return HttpNotFound();
            }
            return View(activityModel);
        }

        // POST: ActivityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ActivityModel activityModel = db.ActivityModels.Find(id);
            db.ActivityModels.Remove(activityModel);
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
