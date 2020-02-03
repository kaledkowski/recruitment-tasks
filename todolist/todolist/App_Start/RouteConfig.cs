using System.Web.Mvc;
using System.Web.Routing;

namespace todolist
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("AddCard", "api/card/", new { controller = "Api", action = "AddCard" }, new { httpMethod = new HttpMethodConstraint("POST") });
            routes.MapRoute("UpdateCard", "api/card/", new { controller = "Api", action = "UpdateCard" }, new { httpMethod = new HttpMethodConstraint("PUT") });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
