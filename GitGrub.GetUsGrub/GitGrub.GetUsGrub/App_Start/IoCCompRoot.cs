//using Autofac;
//using Autofac.Integration.WebApi;
//using GitGrub.GetUsGrub.BusinessLogic;
//using GitGrub.GetUsGrub.DataAccess;
//using GitGrub.GetUsGrub.Models;
//using System.Reflection;
//using System.Web.Http;

//namespace GitGrub.GetUsGrub
//{
//    public class IoCCompRoot
//    {
//        public static void Configure()
//        {
//            var builder = new ContainerBuilder();
//            var config = GlobalConfiguration.Configuration;

//            // Registering Web API controllers
//            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

//            // Binding interfaces to their objects
//            builder.RegisterType<UserAccount>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<SecurityQuestion>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<SecurityAnswerSalt>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<RestaurantAccount>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<PasswordSalt>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<ClaimsModel>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<BusinessHour>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<Address>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<RegisterUserDto>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<RegisterRestaurantUserDto>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<CreateUserManager>().AsImplementedInterfaces().InstancePerRequest();
//            // TODO: Need to figure out how to differentiate this
//            builder.RegisterType<UserGateway>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<RegisterUserPostLogicStrategy>().AsImplementedInterfaces().InstancePerRequest();
//            builder.RegisterType<RegisterUserPreLogicStrategy>().AsImplementedInterfaces().InstancePerRequest();

//            var container = builder.Build();

//            // TODO: If want to do a WebAPI filter
//            // builder.RegisterWebApiFilterProvider(config);
//            // Registering model binding provider
//            builder.RegisterWebApiModelBinderProvider();
//            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
//        }
//    }
//}