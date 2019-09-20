
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

        public void ConfigureContainer(ContainerBuilder builder) => builder.RegisterModule<AutofacModule>();

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
