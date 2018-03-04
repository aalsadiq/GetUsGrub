using Autofac;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // https://www.youtube.com/watch?v=nsnuIZX1C9A&index=15&list=PL6n9fhu94yhW7yoUOGNOfHurUE6bpOO2b
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:50016");

            // IoC Container
            var builder = new ContainerBuilder();
        }
    }
}
