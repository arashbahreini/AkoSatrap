using AkoSatrap.UIHelper;
using DomainDeriven;
using System;
using System.Collections.Generic;
using System.IO;
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

        public JsonResult GetImages()
        {
            var returnResult = new ViewModel.ReturnResult<List<string>>();
            var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/AkoSatrapImages/abilities/");
            if (Directory.Exists(path))
            {
                returnResult.Data = new List<string>();
                if (Directory.GetFiles(path).Any())
                    returnResult.Data = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
            }
            return Json(returnResult);
        }

        public JsonResult DeleteImage(string fileName)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            System.IO.File.Delete(Server.MapPath($"~/AkoSatrapImages/abilities/{fileName}"));
            return Json(returnResult);
        }

        public JsonResult AddImage(FileAttachment fileAttachment, int id)
        {
            var returnResult = new ViewModel.ReturnResult<bool>();

            if (fileAttachment.Attachment.ContentLength > 2097152)
            {
                returnResult.SetError("حجم فایل نمیتواند بیشتر از 2 مگابایت باشد");
                return Json(returnResult);
            }

            var path = Server.MapPath($"~/AkoSatrapImages/abilities/");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            fileAttachment.Attachment.SaveAs($"{path}/{Guid.NewGuid()}.{fileAttachment.Attachment.FileName.Split('.')[1].ToString()}");

            return Json(returnResult);
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