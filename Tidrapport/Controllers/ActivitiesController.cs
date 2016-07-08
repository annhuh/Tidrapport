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
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var project = repository.GetProject((int)id);
                ViewBag.ProjectName = project.Name;

                var activities = repository.GetAllActivitiesForProjectIncludeProject((int)id);
                
                return View(activities.ToList());
            }
            else
            {
                ViewBag.ProjectName = "*";
                var activities = repository.GetAllActivitiesIncludeProject();

                return View(activities.ToList());
            }
        }

        // GET: Activities
        public ActionResult ProjectActivities(int cid, int pid)
        {
            var activities = repository.GetAllActivitiesForProjectIncludeProject(pid);

            var project = repository.GetProject(pid);
            ViewBag.ProjectName = project.Name;
            ViewBag.CustomerId = cid;

            return View(activities);
        }

        public ActionResult Activities (int id)
        {
            var projectId = id;

            return Json(
                repository.GetAllActivitiesForProjectIncludeProject(projectId)
                .Where(a => a.IsActive)
                .Select(a => new
                {
                    value = a.Id,
                    text = a.Name
                }), JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(int? id)
        {
            

            if (id != null)
            {
                int projectId = (int)id;
                var project = repository.GetProject(projectId);

                var projectList = new List<Project>();
                projectList.Add(project);

                ViewBag.ProjectId = new SelectList(projectList, "ProjectId", "Name");
                ViewBag.ProjectName = project.Name;

                return View();
            }
            else
            {
                ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Name");
                ViewBag.ProjectName = "*";

                return View(); 
            }
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
                ViewBag.ProjectName = activity.Project.Name;
                return RedirectToAction("Index", activity.ProjectId);
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
            ViewBag.ProjectId = new SelectList(repository.GetAllProjects(), "ProjectId", "Name", activity.ProjectId);
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
                
                return RedirectToAction("Index", activity.ProjectId);
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
