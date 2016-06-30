using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Dal;
using Tidrapport.Models;


namespace Tidrapport.Controllers
{
    public class ActivitiesController : Controller
    {
        private IRepository repository;

        public ActivitiesController ()
        {
            repository = new Repository();
        }

        public ActivitiesController (IRepository rep)
        {
            repository = rep;
        }


        // GET: Activities
        public ActionResult Index()
        {
            var activities = repository.GetAllActivities();
   
            return View(activities.ToList());
        }

        // GET: Activities
        public ActionResult ProjectActivities(int? cid, int? pid)
        {
            var activities = repository.GetAllActivities();

            ViewBag.CustomerId = cid;
            
            var projectActivities = from activity in activities
                                    where activity.ProjectId == pid
                                    orderby activity.Name
                                    select activity;
             
            return View(projectActivities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = repository.GetActivity((int)id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Number");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsActive,ProjectId,BalanceEffect")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                repository.AddActivity(activity);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Number", activity.ProjectId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = repository.GetActivity((int)id);

            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Number", activity.ProjectId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsActive,ProjectId,BalanceEffect")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateActivity(activity);
                
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Number", activity.ProjectId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Activity activity = repository.GetActivity((int)id);

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteActivity(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
