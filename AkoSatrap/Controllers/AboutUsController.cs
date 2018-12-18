using DomainDeriven;
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
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 5).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }

        public ActionResult CeoMessage()
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 4).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }

        
        public ActionResult Abilities()
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == 6).FirstOrDefault();
                if (result == null) return View(new SiteContent { Body = "با عرض پوزش، صفحه در حال بروزرسانی می باشد" });
                return View(result);
            }
        }
    }
}