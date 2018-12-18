using DomainDeriven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace AkoSatrap.Controllers
{
    public class SiteContentController : Controller
    {
        public JsonResult GetContent(SiteContentModel model)
        {
            using (var dbContext = new AkoSatrapDb())
            {
                var result = dbContext.SiteContents.Where(x => x.PageId == model.PageId).FirstOrDefault();
                return Json(result);
            }
            
        }

        public JsonResult SaveContent(SiteContentModel model)
        {
            try
            {
                using (var dbContext = new AkoSatrapDb())
                {
                    var item =
                        dbContext.SiteContents.Any(x => x.PageId == model.PageId) ?
                        dbContext.SiteContents.Where(x => x.PageId == model.PageId).FirstOrDefault() :
                        new SiteContent();

                    item.PageId = model.PageId;
                    item.Title = model.Title;
                    item.Body = model.Body;

                    if (!dbContext.SiteContents.Any(x => x.PageId == model.PageId))
                    {
                        dbContext.SiteContents.Add(item);
                    }

                    dbContext.SaveChanges();
                }
                return Json("عملیات با موفقیت انجام شد");
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}