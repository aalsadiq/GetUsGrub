using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CSULB.GetUsGrub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable Cors
            config.EnableCors();

            config.Routes.IgnoreRoute("Images Route", "Images/{file}.jpg");

            // Registering global handlers
            config.MessageHandlers.Add(new AuthenticationHandler());

            // Mapping message handler to SSO Controller
            config.Routes.MapHttpRoute(
                name: "SsoRoute",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "Sso", id = RouteParameter.Optional },
                constraints: new { controller = "Sso" },
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[]
                    {
                        new SsoTokenHandler()
                    })
            );

            // Web API attribute routing
            config.MapHttpAttributeRoutes();

            // Web API convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null
            );

            // Add GlobalSecurityExceptionFilter for User Access Control
            // Last Updated: 03/14/18 by Rachel Dang
            config.Filters.Add(new GlobalSecurityExceptionFilter());

            // Setting up JSON serialization
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }
    }
}