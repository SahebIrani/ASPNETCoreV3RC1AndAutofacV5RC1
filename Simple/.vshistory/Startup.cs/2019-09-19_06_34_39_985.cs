
using System.Threading.Tasks;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Simple.Services;

namespace Simple
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }
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
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   })
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterType<PrintMessages>().As<IPrintMessages>().PropertiesAutowired();
                    })
            ;
    }
}
