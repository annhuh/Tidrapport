using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Tidrapport.Dal;
using System.Web.Mvc;
using Tidrapport.Models;

namespace Tidrapport.Controllers
{
    public class ProjectEmployeesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IRepository repository;

        public ProjectEmployeesController()
        {
            repository = new Repository();
        }

        public ProjectEmployeesController(IRepository rep)
        {
            repository = rep;
        }

        // GET: ProjectEmployees
        //public ActionResult Index()
        //{
        //    var projectEmployees = db.ProjectEmployees
        //        .Include(p => p.Employee)
        //        .Include(p => p.Project);

        //    projectEmployees = from ep in projectEmployees    
        //                       orderby ep.Employee.LastName, ep.Employee.FirstName, ep.Project.Name
        //                       select ep;

        //    return View(projectEmployees.ToList());
        // }

        // GET: Projects
        [Authorize(Roles = "anställd")]
        public ActionResult myProjects()
        {
            // identify the user
            var id = User.Identity.GetUserId();
            int employeeId = int.Parse(id);

            var employee = repository.GetEmployee(employeeId);

            var myProjects = repository.GetProjectsAssignedToEmployee(employeeId);
               
            ViewBag.EmployeeId = employeeId;
            ViewBag.Name = employee.FullName;
            
            return View(myProjects.ToList());
        }

        // GET: Employees (ProjectEmployees) for a project (id)
        public ActionResult Employees(int id)
        {
            var project = repository.GetProject(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectName = project.Name;
            ViewBag.ProjectId = project.ProjectId;

            var projectEmployees = repository.GetEmployeesAssignedToProject(id);

            return View(projectEmployees);
        }

        // GET: Projects (ProjectEmployees) for an employee
        public ActionResult Projects(int id)
        {
            int employeeId = id;
            var projectEmployees = repository.GetProjectsAssignedToEmployee(employeeId);

            var employee = repository.GetEmployee(employeeId);

            ViewBag.EmployeeName = employee.FullName;
            ViewBag.EmployeeId = employee.EmployeeId;

            return View(projectEmployees);
        }

        // GET: ProjectEmployees/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
        //    if (projectEmployee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projectEmployee);
        //}

        // GET: ProjectEmployees/Create
        // connect a project to an employee
        public ActionResult ConnectProjectToEmployee(int id)
        {
            var today = DateTime.Today;

            int employeeId = id;
            Employee employee = repository.GetEmployee(employeeId);
            
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeName = employee.FullName;
            ViewBag.EmployeeId = employee.EmployeeId;

            // get all selectable projects (not templates)
            // -------------------------------------------
            var notTemplateNorTerminatedProjects = repository.GetAssignableProjects();

            //get all already assigned projects for the employee
            //--------------------------------------------------
            var assignedProjects = repository.GetProjectsAssignedToEmployee(employeeId);

            // Nota Bene!
            // ----------
            var selectableProjects = notTemplateNorTerminatedProjects
                .Where(p => !assignedProjects.Any(pe => pe.ProjectId == p.ProjectId));

            ViewBag.ProjectId = new SelectList(selectableProjects, "ProjectId", "Name");

            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectProjectToEmployee([Bind(Include = "EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                repository.AddProjectEmployee(projectEmployee);
                return RedirectToAction("Projects", "ProjectEmployees", new { id = projectEmployee.EmployeeId });
            }

            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
            //ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        // connect an employee to a project
        public ActionResult ConnectEmployeeToProject(int id)
        {
            var projectId = id;
            var project = repository.GetProject(projectId);

            if (project == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (project.EndDate != null)
                {
                    if (project.EndDate > DateTime.Today)
                    {
                        // NOT OK
                        // Project closed
                        return View("Projektet " + project.Name + " är avslutat. Öppna igen för att kunna lägga till nya medlemmar. ");
                    }
                }


                // OK (EndDate == null)
                var employees = repository.GetAllEmployeesIncludeCompany();

                var members = repository.GetEmployeesAssignedToProject(projectId);

                // Nota Bene!
                // ----------
                var selectableEmployees = employees
                    .Where(e => !members.Any(pe => pe.EmployeeId == e.EmployeeId))
                    //.
                    //.Select(e => new SelectListItem
                    //{
                    //    Value = e.EmployeeId.ToString(),
                    //    Text = e.FullName
                    //})
                    .ToList();

                ViewBag.ProjectName = project.Name;
                ViewBag.ProjectId = project.ProjectId;

                ViewBag.EmployeeId = new SelectList(selectableEmployees.ToList(), "EmployeeId", "FullName");

                return View();
                
            }
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectEmployeeToProject([Bind(Include = "EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        {
            var ProjectId = projectEmployee.ProjectId;

            if (ModelState.IsValid)
            {
                repository.AddProjectEmployee(projectEmployee);
                return RedirectToAction("Employees", "ProjectEmployees", new { id = projectEmployee.ProjectId } );
            }

            // redirect to another place????
            return View(projectEmployee);
        }

 
        // POST: ProjectEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(projectEmployee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN", projectEmployee.EmployeeId);
        //    ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Number", projectEmployee.ProjectId);
        //    return View(projectEmployee);
        //}

        // GET: ProjectEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = repository.GetProjectEmployee((int)id);

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
            var projectEmployee = repository.GetProjectEmployee(id);

            repository.DeleteProjectEmployee(id);

            //return RedirectToAction("Index", new { id = projectEmployee.ProjectId });
            return Redirect(Request.UrlReferrer.ToString());

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
