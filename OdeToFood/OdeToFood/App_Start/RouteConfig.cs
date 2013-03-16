using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OdeToFood
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //If a user want to come and search cuisines by country
            //Cuisine/french, /country will be passed to action as parameter

            routes.MapRoute(
                "Cuisine",
                "Cuisine/{name}",
                new { controller = "Cuisine", action = "Search", name = UrlParameter.Optional });


            //Greedy, match almost anythink

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}