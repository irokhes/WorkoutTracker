using System.Web.Mvc;
using System.Web.Routing;

namespace WorkoutTracker.Api.App_Start
{
    public class MvcConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
            "Default", "{controller}/{action}/{id}",
            new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}