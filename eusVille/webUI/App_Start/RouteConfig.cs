using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //enable attribute routing for ASP.NET MVC
            //routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //"Manage", // Route name
        //"Manage/CategoryListController/{action}",
        //new { controller = "CategoryListController", action = "Index" },
        //new[] { "WebUI.Controllers.CategoryListController" });

   //         routes.MapRoute(
   //    name: "Test",
   //    url: "test",
   //    defaults: new { controller = "Test", action = "Index" }
   //);    
          

            routes.MapRoute(
               name: "Comments Index",
               url: "comment/{id}",
               defaults: new { controller = "Comment", action = "Index", id = UrlParameter.Optional}
           );

            routes.MapRoute(
                name: "Comments",
                url: "comment/get/{id}",
                defaults: new { controller = "Comment", action = "Comments", id= UrlParameter.Optional}
            );

            //routes.MapRoute(
            //    name: "Topic - Contact",
            //    url: "topic/contact",
            //    defaults: new { controller = "Home", action = "Contact" },
            //    namespaces: new[] { "webUI.Controllers" }
            //);   

                 

            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "Home", action = "Contact" },
                namespaces: new[] { "webUI.Controllers" }
            );   
            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action="About" },
                namespaces: new[] { "webUI.Controllers" }
            );
                     

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webUI.Controllers" }
            );                    
        }
    }
}
