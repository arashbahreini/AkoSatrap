using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        
        public ActionResult TestProjects()
        {
            return View();
        }

        public ActionResult TestProject()
        {
            return View();
        }
        
    }
}