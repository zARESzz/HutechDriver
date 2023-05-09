using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HutechDriver
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HutechDriver.Controllers" }
            );
            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "HutechDriver.Controllers" }
           );
            routes.MapRoute(
               name: "CategoryProduct",
               url: "danh-muc-san-pham/{id}",
               defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
               namespaces: new[] { "HutechDriver.Controllers" }
           );
            routes.MapRoute(
             name: "DetailNew",
             url: "{alias}-n{id}",
              defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new[] { "HutechDriver.Controllers" }
          );
            routes.MapRoute(
               name: "Products",
               url: "san-pham",
               defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "HutechDriver.Controllers" }
           );
            routes.MapRoute(
               name: "detailProduct",
               url: "chi-tiet/{id}",
               defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "HutechDriver.Controllers" }
           );

        }
    }
}
