using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    public class ProjectEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectEmployees
        public ActionResult Index()
        {
            var projectEmployees = db.ProjectEmployees
                .Include(p => p.Employee)
                .Include(p => p.Project);

            projectEmployees = from ep in projectEmployees    
                               orderby ep.Employee.LastName, ep.Employee.FirstName, ep.Project.Name
                               select ep;
                
            return View(projectEmployees.ToList());
         }

        // GET: Projects
        [Authorize(Roles = "anställd")]
        public ActionResult myProjects()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            var employee = db.Employees.Find(employeeId);

            var myProjects = db.ProjectEmployees
                .Where (pe => pe.EmployeeId == employeeId)
                .Include(pe => pe.Employee)
                .Include(pe => pe.Project);

            ViewBag.EmployeeId = employeeId;
            ViewBag.Name = employee.FullName;
            
            return View(myProjects.ToList());
        }

        // GET: Employees (ProjectEmployees) for a project (id)
        public ActionResult Employees(int id)
        {
            var projectEmployees = db.ProjectEmployees
                .Include(p => p.Employee)
                .Include(p => p.Project);

            var project = db.Projects.Find(id);

            ViewBag.Project = project.Name + "(" + project.Number + ")";
           
            projectEmployees = from ep in projectEmployees
                               where ep.ProjectId == id
                               orderby ep.Employee.LastName, ep.Employee.FirstName, ep.Project.Name
                               select ep;

            return View(projectEmployees.ToList());
        }

        // GET: Projects (ProjectEmployees) for an employee
        public ActionResult Projects(int id)
        {
            int employeeId = id;
            var projectEmployees = db.ProjectEmployees.Include(p => p.Project);

            var employee = db.Employees.Find(id);

            ViewBag.EmployeeName = employee.FullName;
            ViewBag.EmployeeId = employee.EmployeeId;

            projectEmployees = from ep in projectEmployees
                               where ep.EmployeeId == employeeId
                               orderby ep.Project.Name, ep.ProjectId
                               select ep;

            return View(projectEmployees.ToList());
        }

        // GET: ProjectEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        // connect an employee (id) to a project
        public ActionResult Create(int id)
        {
            var now = DateTime.Now;

            int employeeId = id;
            Employee employee = db.Employees.Find(employeeId);

            ViewBag.EmployeeName = employee.FullName;
            ViewBag.EmployeeId = employee.EmployeeId;

            // get all selectable projects (not templates)
            // -------------------------------------------
            var notTemplateNorTerminatedProjects = from project in db.Projects
                                                   where project.IsTemplate == false
                                                   select new
                                                   {
                                                       ProjectId = project.ProjectId,
                                                       Name = project.Name
                                                   };

            //&& DateTime. p.EndDate != null?p.EndDate.Value>=DateTime.Today:false);

            // get all already assigned projects for the employee
            // --------------------------------------------------
            var assignedProjects = from projectEmployee in db.ProjectEmployees
                                   where projectEmployee.EmployeeId == employeeId
                                   select new
                                   {
                                       ProjectId = projectEmployee.ProjectId,
                                       Name = ""
                                   };

            var selectableProjects = notTemplateNorTerminatedProjects
                .Where(p => !assignedProjects.Any(pe => pe.ProjectId == p.ProjectId));
                       
            var theSelectList = selectableProjects;
            
            ViewBag.ProjectId = new SelectList(theSelectList, "ProjectId", "Name");

            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.ProjectEmployees.Add(projectEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            //ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        public ActionResult CreateEmployee(int id)
        {
            // id is the project id
            Employee employee = db.Employees.Find(id);

            //ViewBag.EmployeeName = employee.FullName;
            //ViewBag.EmployeeId = employee.EmployeeId;

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");


            //  TBD
            //var projects = db.Projects;
            //var projectEmployees = db.ProjectEmployees;
            //projectEmployees = from projectEmployee in projectEmployees
            //                   where projectEmployee. == EmployeeId != id


            //projects = from project in projects
            //           where project.IsTemplate == false && project.EndDate == null && project.ProjectId 
            //               !in (
            //           orderby project.Project.Name
            //           select project;
            //????????????????????????????????????????????????????????????????

            ViewBag.ProjectId = new SelectList(db.Projects, "Project", "Name");

            return View();
        }

        // POST: ProjectEmployees/CreateEmployee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                int employeeId = projectEmployee.EmployeeId;
                db.ProjectEmployees.Add(projectEmployee);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = employeeId });
            }

            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            //ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            int employeeId = projectEmployee.EmployeeId;
            db.ProjectEmployees.Remove(projectEmployee);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = employeeId });
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
