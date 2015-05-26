using System.Web.Http;
using System.Web.Routing;
using WorkoutTracker.Api.App_Start;

namespace WorkoutTracker.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MvcConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
