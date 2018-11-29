using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace AkoSatrap.AuthData
{
    public class AuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    filterContext.Result = new HttpUnauthorizedResult();

                    var loginPageUrl = UrlHelper.GenerateUrl(null, "Login", "Authentication", null, RouteTable.Routes, HttpContext.Current.Request.RequestContext, false);
                    var serializer = new JavaScriptSerializer();

                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                    filterContext.HttpContext.Response.ContentType = "application/json";
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.Write(serializer.Serialize(new
                    {
                        Url = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath,
                        LoginUrl = loginPageUrl
                    }));
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/PAuthentication/Login");
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}