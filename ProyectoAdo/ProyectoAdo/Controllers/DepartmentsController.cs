using ProyectoAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAdo.Controllers
{
    public class DepartmentsController : Controller
    {
        private EmployeeContext employeeContext = new EmployeeContext();
        // GET: Departments
        public ActionResult Index()
        {
           
            List<Department> departmentsModel = employeeContext.Departments.ToList();
            return View(departmentsModel);
        }

    }
}