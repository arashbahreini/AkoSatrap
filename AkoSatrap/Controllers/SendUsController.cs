using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AkoSatrap.UIHelper.Captcha;
using System.Web.UI;

namespace AkoSatrap.Controllers
{
    public class SendUsController : Controller
    {
        [NoBrowserCache]
        public ActionResult Index()
        {
            return View();
        }

        [NoBrowserCache]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "None")]
        public CaptchaImageResult CaptchaImage(string rndDate)
        {
            return new CaptchaImageResult();
        }

        [HttpPost]
        public ActionResult Index(UploadFileModel uploadFileModel)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != uploadFileModel.CaptchaInputText)
            {
                ModelState.AddModelError("CaptchaInputText", "مجموع اشتباه است");
                return View(uploadFileModel);
            }

            if (!ModelState.IsValid)
            {
                return View(uploadFileModel);
            }

            if (uploadFileModel.File != null && uploadFileModel.File.ContentLength > 0)
            {
                try
                {
                    var fileType = uploadFileModel.FileType.GetDescription();
                    MailAddress mailfrom = new MailAddress("akosatrapiranian@gmail.com");
                    MailAddress mailto = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mailto"].ToString());
                    MailMessage newmsg = new MailMessage(mailfrom, mailto);

                    newmsg.Subject = $"ارسال از سایت:{fileType}-{uploadFileModel.Title}";
                    string body = $"{uploadFileModel.Description}";
                    body += $"{Environment.NewLine} سیستم ارسال به ما- سایت آکو ساتراپ ایرانیان";


                    newmsg.Body = body;
                    newmsg.BodyEncoding = Encoding.UTF8;

                    //For File Attachment, more file can also be attached
                    Attachment att = new Attachment(uploadFileModel.File.InputStream, Path.GetFileName(uploadFileModel.File.FileName));
                    newmsg.Attachments.Add(att);

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("akosatrapiranian", "870943635");
                    smtp.EnableSsl = true;
                    smtp.Send(newmsg);
                }
                catch (Exception ex)
                {
                    var s = "عملیات با خطا مواجه شد" + ex.Message.ToString();
                    return View("Result", (object)s);
                }
            }
            return View("Result", (object)"فایل با موفقیت ارسال شد");
        }
    }
}