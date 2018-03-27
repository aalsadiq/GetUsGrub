using CSULB.GetUsGrub.UserAccessControl;
using System.Web.Http;

namespace CSULB.GetUsGrub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add GlobalSecurityExceptionFilter for User Access Control
            // Last Updated: 03/14/18 by Rachel Dang
            config.Filters.Add(new GlobalSecurityExceptionFilter());
        }
    }
}
