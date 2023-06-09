using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HutechDriver.Controllers;
using HutechDriver.Models;

namespace HutechDriver
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
            Database.SetInitializer<ApplicationDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException httpException && httpException.GetHttpCode() == 404)
            {
                Response.Clear();
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                RouteData routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "NotFound";
                IController errorController = new ErrorController();
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }
    }
}
