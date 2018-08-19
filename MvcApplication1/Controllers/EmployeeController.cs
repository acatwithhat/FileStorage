using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using NHibernate.Linq;
using NHibernate;

namespace MvcApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            using (ISession session = NHibertnateSession.OpenSession())
            {
                var employees = session.Query<Employee>().ToList();
                return View(employees);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(/*Employee employee, HttpPostedFileBase upload*/ EmployeeVM employeevm)
        {
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {

                        Employee employee = new Employee(employeevm);
                        session.Save(employee);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }
        
        public ActionResult OpenFile(Employee employee){
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    var employees = session.Get<Employee>(employee.Id);

                    return File(employees.BFile, System.Net.Mime.MediaTypeNames.Application.Octet, "kek.jpg");
                }
                
            }
            catch (Exception exc)
            {
                return View();
            }
            
        }

        public ActionResult Image()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Image(HttpPostedFileBase file)
        {
            HttpPostedFileBase file1 = file;
            return View();
        }

    }
}
