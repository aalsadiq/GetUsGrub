namespace GitGrub.GetUsGrub
{
    public static class IoCConfig
    {
        //// Enums for multiple types
        //public enum SaltType
        //{
        //    PasswordSalt,
        //    TokenSalt
        //}

        //public static void Configure()
        //{
        //    var builder = new ContainerBuilder();
        //    var config = GlobalConfiguration.Configuration;

        //    // Registering Web API controllers
        //    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


        //    // Binding interfaces to their objects
        //    builder.RegisterType<CreateUserManager>().AsImplementedInterfaces().InstancePerRequest();
        //    // Need to figure out how to differentiate this
        //    builder.RegisterType<UserGateway>().AsImplementedInterfaces().InstancePerRequest();
        //    //
        //    builder.RegisterType<PasswordSalt>().Keyed<ISalt>(SaltType.PasswordSalt).AsImplementedInterfaces().InstancePerRequest();
        //    builder.RegisterType<TokenSalt>().Keyed<ISalt>(SaltType.TokenSalt).AsImplementedInterfaces().InstancePerRequest();

        //    var container = builder.Build();

        //    // builder.RegisterWebApiFilterProvider(config);
        //    // Registering model binding provider
        //    builder.RegisterWebApiModelBinderProvider();
        //    config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        //}
    }
}