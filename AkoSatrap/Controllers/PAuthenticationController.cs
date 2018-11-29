using AkoSatrap.AuthData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace AkoSatrap.Controllers
{
    public class PAuthenticationController : Controller
    {
        // GET: PAuthentication
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login()
        {
            var user = HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "PHome");//new HttpUnauthorizedResult();
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(Login loginModel)
        {
            var loginResult = login(loginModel);
            if (loginResult)
            {
                return RedirectToAction("Index", "PHome");
            }
            else
            {
                return RedirectToAction("Login", "PAuthentication");
            }

            //Response.Redirect("~/");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            logout();
            //var user = HttpContext.User as CustomPrincipal;
            //var userConnection = MMSNotificationHub.UserList.Where(r => r.Key.UserId == user.UserId).FirstOrDefault();

            //HubContext.Clients.Client(userConnection.Value).userDisconnected();                        
            return RedirectToAction("Login", "PAuthentication");
            //Response.Redirect("~/Authentication/Login");
        }

        public bool login(Login loginModel)
        {
            bool correctLogin = false;

            if (loginModel.UserName=="akosatrap" && loginModel.Password=="123456")
            {
                Session["token"] = "";
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = 1;
                serializeModel.FirstName = "محسن";
                serializeModel.LastName = "جعفری";
                serializeModel.FullName = "محسن جعفری";
                serializeModel.PartnerId = 1;
                serializeModel.EmployeeID = 1;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                         serializeModel.UserId.ToString(),
                         DateTime.Now,
                         DateTime.Now.AddMinutes(30),
                         false,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                //faCookie.Expires = authTicket.Expiration;

                Response.Cookies.Add(faCookie);

                correctLogin = true;
            }

            return correctLogin;
        }

        public void logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                Response.Cookies.Add(aCookie); // overwrite it
            }
        }
    }
}