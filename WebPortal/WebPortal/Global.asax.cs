using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

using WebPortal.App_Start;

namespace WebPortal
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }

        protected void Application_Start()
        {
            // Don't make empty strings in JSON to nulls
            // http://stackoverflow.com/questions/12734083/string-empty-converted-to-null-when-passing-json-object-to-mvc-controller
            ModelBinders.Binders.DefaultBinder = new EmptyStringModelBinder();
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}