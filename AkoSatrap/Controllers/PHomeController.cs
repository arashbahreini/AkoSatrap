using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class PHomeController : SecurityController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}