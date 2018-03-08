using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Configure Web API to use only bearer token authentication
            //config.SupressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // https://www.youtube.com/watch?v=nsnuIZX1C9A&index=15&list=PL6n9fhu94yhW7yoUOGNOfHurUE6bpOO2b
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:50016");

            // Json Serializer Settings
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));

        #if !DEBUG
        // Require HTTPS on entire API
                    config.Filters.Add(new RequireHttpsAttribute());
        #endif
        }
    }
}
