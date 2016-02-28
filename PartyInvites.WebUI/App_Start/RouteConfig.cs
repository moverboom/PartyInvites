using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartyInvites.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new { controller = "Home", action = "Index" });

            routes.MapRoute(null,
                "Response/Responses",
                new { controller = "Response", action = "Responses" });

            routes.MapRoute(null,
                "Response/Responses/Page{page}",
                new { controller = "Response", action = "Responses", filter = (string)null },
                new { page = @"\d+" });

            routes.MapRoute(null,
                "Response/Responses/{filter}",
                new { controller = "Response", action = "Responses", page = 1 });

            routes.MapRoute(null,
                "Response/Responses/{filter}/Page{page}",
                new { controller = "Response", action = "Responses" },
                new { page = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
