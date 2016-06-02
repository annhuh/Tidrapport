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
            var projectEmployees = db.ProjectEmployees.Include(p => p.Employee).Include(p => p.Project);
            projectEmployees = from ep in projectEmployees    
                               orderby ep.Employee.LastName, ep.Employee.FirstName, ep.Project.Name
                               select ep;
                
            return View(projectEmployees.ToList());
         }

        // GET: Projects
        public ActionResult myProjects()
        {
            var id = User.Identity.GetUserId();

            int employeeId = int.Parse(id);
            var employee = db.Employees.Find(employeeId);

            var myProjects = db.ProjectEmployees.Where (pe => pe.EmployeeId == employeeId).Include(pe => pe.Employee).Include(pe => pe.Project);

            ViewBag.EmployeeId = employeeId;
            ViewBag.Name = employee.FullName;
            
                return View(myProjects.ToList());
        }

        // GET: Employees (ProjectEmployees) for a project (id)
        public ActionResult Employees(int id)
        {
            var projectEmployees = db.ProjectEmployees.Include(p => p.Employee).Include(p => p.Project);

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
            int employeeId = id;
            Employee employee = db.Employees.Find(employeeId);

            ViewBag.EmployeeName = employee.FullName;
            ViewBag.EmployeeID = employee.EmployeeId;

            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "SSN");
            
            var allProjects = db.Projects;
            var allProjectEmployees = db.ProjectEmployees;
            var now = DateTime.Now;

            // all assignable projects
            var notTemplateNorTerminatedProjects = from p in allProjects
                                                   where !p.IsTemplate // && (p.EndDate < now)
                                                   select p;
 
            var assignedProjects = from assignedProject in allProjectEmployees 
                                   where assignedProject.EmployeeId == employeeId
                                   select assignedProject;
      
            var alreadyAssignedProjects = from ep in notTemplateNorTerminatedProjects
                                          join aap in assignedProjects
                                          on ep.ProjectId equals aap.ProjectId
                                          select new { 
                                                    ProjectId = ep.ProjectId,
                                                    ProjectName = ep.Name
                                          };
            
            var theSelectList = notTemplateNorTerminatedProjects.ToList();
            
            theSelectList.Insert(0, new Project()
                {
                    Name = "Ange val",
                    ProjectId = -1
                });

            //ViewBag.ProjectId = new SelectList(notTemplateNorTerminatedProjects, "ProjectId", "Name", alreadyAssignedProjects);
            ViewBag.ProjectId = new SelectList(theSelectList, "ProjectId", "Name", alreadyAssignedProjects);
           
            //var projectsToExclude = from ep in notTemplateNorTerminatedProjects
            //                        join pe in allProjectEmployees
            //                        on ep.

            //var projectEmployees = db.ProjectEmployees;
            //projectEmployees = from projectEmployee in projectEmployees
            //                   where projectEmployee.EmployeeId Equals 


            //projects = from project in projects
            //           where !project.IsTemplate && (project.EndDate == null || project.EndDate <  project.DateTime.Now) 
            //           orderby project.Project.Name
            //           select project;
          
            
            //var projList = new SelectList(projects, "ProjectId", "Name");
            //var selectList = new List (new SelectListItem
            //{
            //    Value = "-1",
            //    Text = "Välj alternativ"
            //});
            
            //ViewBag.ProjectId = new SelectList(db.Projects, "Project", "Name");


            // start
            //var selectList = new List<SelectListItem>();
                  
            //var allProjects = db.Projects;
            //var allProjectEmployees = db.ProjectEmployees;

            //selectList.Insert(0, new SelectListItem
            //{
            //    Value = "-1",
            //    Text = "Välj alternativ"
            //});

            //if (id == null)
            //{
            //    foreach (var item in allProjects)
            //    {
            //        selectList.Add(new SelectListItem
            //        {
            //            Value = item.Id.ToString(),
            //            Text = item.SSN //item.FirstName + " " + item.LastName
            //        });
            //    }
            //}
            //else
            //{
            //    bool isInCourse = false;

            //    foreach (var sItem in Students)
            //    {
            //        // Check if student is already in course
            //        foreach (var scItem in StudentCourses)
            //        {
            //            if (scItem.CourseId == id && scItem.StudentId == sItem.Id)
            //            {
            //                isInCourse = true;
            //            }
            //        }

            //        if (isInCourse == false)
            //        {
            //            selectList.Add(new SelectListItem
            //            {
            //                Value = sItem.Id.ToString(),
            //                Text = sItem.SSN //sItem.FirstName + " " + sItem.LastName
            //            });
            //        }

            //        isInCourse = false;
            //    }
            //}

            //return selectList;
            /////// end 

            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,ProjectId")] ProjectEmployee projectEmployee)
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
