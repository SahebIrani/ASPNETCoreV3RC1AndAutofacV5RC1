using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Simple.Services;

using TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules;

namespace Simple
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called.
            //services.AddAutofac();
            services.AddOptions( );

            // This adds the required middleware to the ROOT CONTAINER and is required for multitenancy to work.
            //AddAutofacMultitenantRequestServices();
            //AutofacChildScopeServiceProviderFactory();
            // AutofacChildLifetimeScopeConfigurationAdapter();

            var adapter = new AutofacChildLifetimeScopeConfigurationAdapter();
            var actions = new AutofacChildLifetimeScopeConfigurationAdapter();
            adapter.Add(builder => { });
            actions.Add(builder => builder.Populate(services));
            //AutofacContainer = services.BuildServiceProvider().GetAutofacRoot(); // Singleton BuildServiceProvider
            var factory = new AutofacChildLifetimeScopeServiceProviderFactory(GetRootLifetimeScope);
            var factory2 = new AutofacChildLifetimeScopeServiceProviderFactory(GetRootLifetimeScopeWithDependency<IPrintMessages>(typeof(IPrintMessages)));
            var myServices = new ServiceCollection().AddTransient<IPrintMessages>();
            services.AddSingleton<IPrintMessages>( );
            var configurationAdapter = factory.CreateBuilder(services);
            var serviceProvider = factory.CreateServiceProvider(configurationAdapter);
            var builder = new ContainerBuilder();
            foreach( var action in configurationAdapter.ConfigurationActions )
            {
                action(builder);
            }
            configurationAdapter.Add(builder => builder.RegisterType<IPrintMessages>( ));
            //var service = serviceProvider.GetRequiredService<IPrintMessages>();
        }


        // This only gets called if your environment is Development. The
        // default ConfigureServices won't be automatically called if this
        // one is called.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // Add things to the service collection that are only for the
            // development environment.
        }

        private static ILifetimeScope GetRootLifetimeScope() => new ContainerBuilder( ).Build( );

        private static ILifetimeScope GetRootLifetimeScopeWithDependency<TAs>(Type type)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType(type).As<TAs>( );
            return containerBuilder.Build( );
        }

        // Use this method to add services directly to LightInject
        // Important: This method must exist in order to replace the default provider.
        //public void ConfigureContainer(IServiceContainer container)
        //{
        //	container.RegisterFrom<CompositionRoot>();
        //}

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.
            //builder.RegisterModule(new AutofacModule());

            builder.RegisterModule<AutofacModule>( );
        }

        // This only gets called if your environment is Production. The
        // default ConfigureContainer won't be automatically called if this
        // one is called.
        public void ConfigureProductionContainer(ContainerBuilder builder)
        {
            // Add things to the ContainerBuilder that are only for the
            // production environment.
        }

        // This only gets called if your environment is Staging. The
        // default Configure won't be automatically called if this one is called.
        public void ConfigureStaging(IApplicationBuilder app , ILoggerFactory loggerFactory)
        {
            // Set up the application for staging.
        }

        // Here's the change for child lifetime scope usage! Register your "root"
        // child lifetime scope things with the adapter.
        //public void ConfigureContainer(AutofacChildLifetimeScopeConfigurationAdapter config)
        //{
        //    config.Add(builder => builder.RegisterModule(new AutofacModule()));
        //}

        //public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        //{
        //    // This is the MULTITENANT PART. Set up your tenant-specific stuff here.
        //    var strategy = new MyTenantIdentificationStrategy();
        //    var mtc = new MultitenantContainer(strategy, container);
        //    mtc.ConfigureTenant("a" , cb => cb.RegisterType<TenantDependency>( ).As<IDependency>( ));
        //    return mtc;
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            //if (!(serviceProvider is AutofacServiceProvider autofacServiceProvider))
            //    throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, ServiceProviderExtensionsResources.WrongProviderType, serviceProvider?.GetType()));
            //return autofacServiceProvider.LifetimeScope;
            //AutofacContainer.ChildLifetimeScopeBeginning
            AutofacContainer = app.ApplicationServices.GetAutofacRoot( );
            if( AutofacContainer.IsRegistered<IPrintMessages>( ) )
            {
                app.Use(async (context , next) =>
                {
                    IPrintMessages service = app.ApplicationServices.GetRequiredService<IPrintMessages>();
                    string newContent = service.Print() + service.Print(" SinjulMSBH .. !!!!");
                    await context.Response.WriteAsync(newContent);
                });
            }
        }
    }
}
