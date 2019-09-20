
using System.Threading.Tasks;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Simple
{
    public interface IPrintMessages
    {
        string Print() => "JackSLater , ";
        string Print(string? message) => message ?? Print();
    }
    public class Program
    {
        public static async Task Main(string[] args) =>
           await Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterType<IPrintMessages>().AsSelf())
          .Build()
          .RunAsync()
          ;
    }
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
                    string newContent = service.Print() + service.Print("SinjulMSBH .. !!!!");
                    await context.Response.WriteAsync(newContent);
                });
            }
        }
    }

}


