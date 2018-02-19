using Autofac;
using Autofac.Integration.WebApi;
using GitGrub.GetUsGrub.DataAccess.Gateways;
using GitGrub.GetUsGrub.Managers;
using System.Reflection;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    public class IoCConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Registering Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Binding interfaces to their objects
            builder.RegisterType<CreateUserManager>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<ValidateGateway>().AsImplementedInterfaces().InstancePerRequest();

            var container = builder.Build();
            // builder.RegisterWebApiFilterProvider(config);
            // Registering model binding provider
            builder.RegisterWebApiModelBinderProvider();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}