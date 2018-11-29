using System.Web.Mvc;
using AkoSatrap.UIHelper.Captcha;
using System.Web.UI;

namespace AkoSatrap.Controllers
{
    public class DefaultController : Controller
    {
        [NoBrowserCache]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateCaptchaAttribute, ValidateAntiForgeryToken]
        public ActionResult Index(LogOnModel model)
        {
            if (!ModelState.IsValid) return View(model);

            TempData["message"] = "کد امنیتی را به درستی وارد کرده اید";

            return View(new LogOnModel { UserName = "", CaptchaInputText = "" });
        }

        [NoBrowserCache]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "None")]
        public CaptchaImageResult CaptchaImage(string rndDate)
        {
            return new CaptchaImageResult();
        }
    }
}
