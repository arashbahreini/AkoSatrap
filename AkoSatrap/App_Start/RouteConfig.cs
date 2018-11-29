using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AkoSatrap
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Product", "Product/{id}/{seoName}",
                    new { controller = "Product", action = "Product", seoName = "" },
                        new { id = @"^\d+$" });

            routes.MapRoute("SendUs", "SendUs/{seoName}",
                    new { controller = "SendUs", action = "Index", seoName = "" });


            routes.MapRoute("ContactUs", "ContactUs/{seoName}",
                    new { controller = "Home", action = "ContactUs", seoName = "" });

            routes.MapRoute("AboutUs", "AboutUs/{seoName}",
                    new { controller = "AboutUs", action = "AboutCompany", seoName = "" });

            routes.MapRoute("CeoMessage", "CeoMessage/{seoName}",
                    new { controller = "AboutUs", action = "CeoMessage", seoName = "" });

            routes.MapRoute("Abilities", "Abilities/{seoName}",
                    new { controller = "AboutUs", action = "Abilities", seoName = "" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
