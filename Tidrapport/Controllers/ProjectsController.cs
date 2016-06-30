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
    public class ProjectsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository repository;

        public ProjectsController()
        {
            repository = new Repository();
        }

        public ProjectsController(IRepository rep)
        {
            repository = rep;
        }

        // GET: Projects
        public ActionResult Index(int? id)
        {
            ViewBag.CustomerId = id;
            
            if (id != null)
            {
                var projects = repository.GetAllProjectsForCustomer((int)id);

                return View(projects);
            }
            else {
                var projects = repository.GetAllProjects();

                return View(projects);
            }
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = repository.GetProject ((int)id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var theCustomer = repository.GetCustomer ((int)id);

                List<Customer> customers = new List<Customer>();
                customers.Add(theCustomer);

                ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name");
            }
            else
            {
                var customers = repository.GetAllCustomers();

                ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name");
            }
            
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,Number,Name,StartDate,EndDate,IsTemplate,CustomerId")] Project project)
        {
            if (ModelState.IsValid)
            {
                repository.AddProject(project);
                return RedirectToAction("Index", new { id = project.CustomerId });
            }

            var customers = repository.GetAllCustomers();

            ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name", project.CustomerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = repository.GetProject((int)id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var customers = repository.GetAllCustomers();

            ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name", project.CustomerId);

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Number,Name,StartDate,EndDate,IsTemplate,CustomerId")] Project project)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateProject(project);
                return RedirectToAction("Index");
            }

            var customers = repository.GetAllCustomers();

            ViewBag.CustomerId = new SelectList(customers, "CustomerId", "Name", project.CustomerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = repository.GetProject((int)id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteProject(id);

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
