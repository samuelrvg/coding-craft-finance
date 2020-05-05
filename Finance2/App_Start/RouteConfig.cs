﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Finance2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var rota = routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Finance2.Controllers" }
            );

            rota.DataTokens["UseNamespaceFallback"] = true;
        }
    }
}
