using dbTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dbTest.Controllers
{
    public class DbTestController : BaseController
    {
        // GET: DbTest
        public ActionResult Index()
        {
            var parameters = new Dictionary<string, object>()
            {
                {"ID",1 }
            };
            var list = con.ToList<DbTestViewModel>("getEmployee",parameters);
            return View(list);
        }
    }
}