using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class BannerController : Controller
    {
        [HttpGet]
        public JsonResult GetBanners()
        {
            ViewModel.ReturnResult<List<string>> returnResult = new ViewModel.ReturnResult<List<string>>();
            var path = Server.MapPath($"~/fa-IR/data6/images");
            returnResult.Data = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
            return Json(returnResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetBanner(FileAttachment fileAttachment, string Id)
        {
            var returnResult = new ViewModel.ReturnResult<bool>();

            if (fileAttachment.Attachment.ContentLength > 2097152)
            {
                returnResult.SetError("حجم فایل نمیتواند بیشتر از 2 مگابایت باشد");
                return Json(returnResult);
            }
            var path = Server.MapPath($"~/fa-IR/data6/images/");
            System.IO.Directory.CreateDirectory(path);

            fileAttachment.Attachment.SaveAs($"{path}/{Id}");

            return Json(returnResult);
        }
    }
}