using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult AboutCompany()
        {
            return View();
        }

        public ActionResult CeoMessage()
        {
            return View();
        }

        
        public ActionResult Abilities()
        {
            return View();
        }
    }
}