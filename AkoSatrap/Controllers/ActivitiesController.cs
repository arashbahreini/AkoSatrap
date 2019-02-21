using DomainDeriven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class ActivitiesController : Controller
    {


        public ActionResult Contracting()
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 1).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }

        public ActionResult FireStations()
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 2).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }

        public ActionResult Engineeringsystem()
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 3).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }

    
    }
}