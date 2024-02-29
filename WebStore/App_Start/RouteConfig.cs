using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // TODO: Direct actions automatically from URL?

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SeedImages",
                url: "SeedImages",
                defaults: new { controller = "Images", action = "SeedImages" }
            );

            routes.MapRoute(
                name: "ASP.NET Guide",
                url: "ASPNET_Guide",
                defaults: new { controller = "Home", action = "ASPNET_Guide" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
