using System;
using System.IO;
using System.Linq;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Simple
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // ASP.NET Core 3.0+:
            // The UseServiceProviderFactory call attaches the
            // Autofac provider to the generic hosting mechanism.
            //https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#quick-start-with-configurecontainer
            //https://github.com/autofac/Autofac.Extensions.DependencyInjection/tree/v5.0.0-rc1
            return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder
                               .UseContentRoot(Directory.GetCurrentDirectory())
                               .UseIISIntegration()
                               .UseStartup<Startup>()
                      ;
                   })
                    // The service provider factory used here allows for
                    // ConfigureContainer to be supported in Startup with
                    // a strongly-typed ContainerBuilder.
                    //https://github.com/autofac/Autofac.Extensions.DependencyInjection/blob/c6f14d73afe25c5c0cf1420581921d7c7790426f/src/Autofac.Extensions.DependencyInjection/AutofacServiceProviderFactory.cs#L52-L61
                    //https://github.com/autofac/Autofac.Extensions.DependencyInjection/blob/c6f14d73afe25c5c0cf1420581921d7c7790426f/src/Autofac.Extensions.DependencyInjection/ServiceCollectionExtensions.cs#L42-L45
                    .ConfigureServices(services => services.AddAutofac())
                    //public static IServiceCollection AddAutofac(this IServiceCollection services, Action<ContainerBuilder> configurationAction = null)
                    //{
                    //	return services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory(configurationAction));
                    //}
                    //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        //builder.RegisterType<TestService>().As<ITestService>().PropertiesAutowired();
                        //builder.RegisterType<TestRepository>().As<ITestRepository>().InstancePerLifetimeScope();

                        Type[] controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

                        builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

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
}
