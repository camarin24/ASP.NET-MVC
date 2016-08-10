using ProyectoAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAdo.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeContext employeeContext = new EmployeeContext();
        public ActionResult Index(int id)
        {
            
            List<Employee> employeeModel = employeeContext.Employees.Where(m => m.DepartmentId == id).ToList();
            return View(employeeModel);
        }

        public ActionResult Detail(int id)
        {

            Employee employeeModel = employeeContext.Employees.Single(m => m.ID == id);
            return View(employeeModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}