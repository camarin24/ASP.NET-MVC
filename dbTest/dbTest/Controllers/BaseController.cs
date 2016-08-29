using dbTest.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dbTest.Controllers
{
    public class BaseController : Controller
    {
        protected DataAcces con = new DataAcces();
    }
}