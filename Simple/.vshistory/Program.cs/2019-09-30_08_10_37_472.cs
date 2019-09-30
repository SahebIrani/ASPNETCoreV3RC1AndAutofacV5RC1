using System;
using System.Linq;
using System.Threading.Tasks;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using Simple.Services;

using TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules;

namespace Simple
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build( ).RunAsync( );

        //public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
        //  BlazorWebAssemblyHost.CreateDefaultBuilder( )
        //    .UseServiceProviderFactory(new AutofacServiceProviderFactory(Register))
        //    .UseBlazorStartup<Startup>( );

        private static void Register(ContainerBuilder builder)
        {
            // add any registrations here
            builder.RegisterModule<AutofacModule>( );
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // ASP.NET Core 3.0+:
            // The UseServiceProviderFactory call attaches the
            // Autofac provider to the generic hosting mechanism.
            //https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#quick-start-with-configurecontainer
            //https://github.com/autofac/Autofac.Extensions.DependencyInjection/tree/v5.0.0-rc1
            Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.ConfigureServices(services => services.AddAutofac( ));

                       webBuilder.UseStartup<Startup>( );
                   })
                    //https://github.com/autofac/Autofac.Extensions.DependencyInjection/pull/52
                    //.UseAutofacChildScopeFactory()
                    // The service provider factory used here allows for
                    // ConfigureContainer to be supported in Startup with
                    // a strongly-typed ContainerBuilder.
                    //https://github.com/autofac/Autofac.Extensions.DependencyInjection/blob/c6f14d73afe25c5c0cf1420581921d7c7790426f/src/Autofac.Extensions.DependencyInjection/AutofacServiceProviderFactory.cs#L52-L61
                    //https://github.com/autofac/Autofac.Extensions.DependencyInjection/blob/c6f14d73afe25c5c0cf1420581921d7c7790426f/src/Autofac.Extensions.DependencyInjection/ServiceCollectionExtensions.cs#L42-L45
                    //.ConfigureServices(services => services.AddAutofac())
                    //HostBuilder, call UseAutofac
                    //public static IServiceCollection AddAutofac(this IServiceCollection services, Action<ContainerBuilder> configurationAction = null)
                    //{
                    //	return services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory(configurationAction));
                    //}

                    //services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory());
                    //.UseServiceProviderFactory(new AutofacChildLifetimeScopeServiceProviderFactory())
                    //.UseServiceProviderFactory<AutofacServiceProviderFactory>() //IServiceProviderFactory<ContainerBuilder> //Add Singleton
                    //.UseServiceProviderFactory(new AutofacMultitenantServiceProviderFactory(Startup.ConfigureMultitenantContainer))
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory( ))
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory(Register))
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        //builder.RegisterType<TestService>().As<ITestService>().PropertiesAutowired();
                        //builder.RegisterType<TestRepository>().As<ITestRepository>().InstancePerLifetimeScope();

                        Type[] controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

                        builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired( );

                        builder.RegisterType<PrintMessages>( ).As<IPrintMessages>( ).PropertiesAutowired( );

                        //IContainer container = builder.Build();
                        //ITestService testService = container.Resolve<ITestService>();
                        //string result = testService.PrintTest("SinulMSBH");

                        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.1
                        //https://mderriey.com/2018/08/02/autofac-integration-in-asp-net-core-generic-hosts/
                        // registering services in the Autofac ContainerBuilder
                        //System.InvalidCastException: 'Unable to cast object of type 'Microsoft.Extensions.DependencyInjection.ServiceCollection' to type 'Autofac.ContainerBuilder'.'
                    })
            ;
    }
}
