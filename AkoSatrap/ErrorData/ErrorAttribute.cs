using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace AkoSatrap.ErrorData
{
    public class ErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //var javaScriptResult = new JavaScriptResult();
                //javaScriptResult.Script = "window.location.href = '../Error'";
                //filterContext.Result =  javaScriptResult;
                //filterContext.HttpContext.Response.Clear();
                //filterContext.HttpContext.Response.StatusCode = 400;                
                var loginPageUrl = UrlHelper.GenerateUrl(null, "Index", "Error", null, RouteTable.Routes, HttpContext.Current.Request.RequestContext, false);
                var serializer = new JavaScriptSerializer();

                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.ContentType = "application/json";
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.HttpContext.Response.Write(serializer.Serialize(new
                {
                    Url = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath,
                    LoginUrl = loginPageUrl
                }));
                filterContext.HttpContext.Response.End();
            }
            else
            {
                filterContext.Result = new RedirectResult("/Error/");
            }
        }
    }
}