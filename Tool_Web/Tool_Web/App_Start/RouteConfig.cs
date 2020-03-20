using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tool_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "RegisteredVaild",
                url: "RegisteredVaild/{VaildID}/{IsVue}",
                defaults: new { controller = "Login", action = "RegisteredVaild", VaildID = UrlParameter.Optional, IsVue = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ResendMail",
                url: "ResendMail/{UID}/{Email}",
                defaults: new { controller = "Login", action = "ResendMail"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{Name}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, Name = UrlParameter.Optional }
            );
        }
    }
}
