using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Simple.Services;

using TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules;

namespace Simple
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var adapter = new AutofacChildLifetimeScopeConfigurationAdapter();
            var actions = new AutofacChildLifetimeScopeConfigurationAdapter();
            adapter.Add(builder => { });
            actions.Add(builder => builder.Populate(services));
            var factory = new AutofacChildLifetimeScopeServiceProviderFactory(GetRootLifetimeScope);
            var factory2 = new AutofacChildLifetimeScopeServiceProviderFactory(GetRootLifetimeScopeWithDependency<IPrintMessages>(typeof(IPrintMessages)));
            var myServices = new ServiceCollection().AddTransient<IPrintMessages>();
            services.AddSingleton<IPrintMessages>();
            var configurationAdapter = factory.CreateBuilder(services);
            var serviceProvider = factory.CreateServiceProvider(configurationAdapter);
            var builder = new ContainerBuilder();
            foreach (var action in configurationAdapter.ConfigurationActions)
            {
                action(builder);
            }
            configurationAdapter.Add(builder => builder.RegisterType<IPrintMessages>());
        }

        private static ILifetimeScope GetRootLifetimeScope() => new ContainerBuilder().Build();

        private static ILifetimeScope GetRootLifetimeScopeWithDependency<TAs>(Type type)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType(type).As<TAs>();
            return containerBuilder.Build();
        }

        public void ConfigureContainer(ContainerBuilder builder) => builder.RegisterModule<AutofacModule>();

        public void ConfigureProductionContainer(ContainerBuilder builder) { }

        public void Configure(IApplicationBuilder app)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (AutofacContainer.IsRegistered<IPrintMessages>())
            {
                app.Use(async (context, next) =>
                {
                    IPrintMessages service = app.ApplicationServices.GetRequiredService<IPrintMessages>();
                    string newContent = service.Print() + service.Print(" SinjulMSBH .. !!!!");
                    await context.Response.WriteAsync(newContent);
                });
            }
        }
    }
}
