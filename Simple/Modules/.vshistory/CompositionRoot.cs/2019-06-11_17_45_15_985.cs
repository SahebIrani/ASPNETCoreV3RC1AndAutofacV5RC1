using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules
{
	//public class CompositionRoot : ICompositionRoot
	//{
	//	public void Compose(IServiceRegistry serviceRegistry)
	//	{
	//		serviceRegistry.Register<IFoo, Foo>();
	//	}
	//}
	//public interface IFoo { }

	//public class Foo : IFoo { }


	//LightInject container

	//var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
	//var container = new ServiceContainer(containerOptions);
	//container.Register<IProductService, ProductManager>();
	//services.AddMvc();
	//return container.CreateServiceProvider(services);

	//var containerOptions = new ContainerOptions { EnablePropertyInjection = false, };
	//var container = new ServiceContainer(containerOptions);

	//container.Intercept(sr => sr.ServiceType ==
	//		typeof(IServis), (sf, pd) => pd.Implement(() => new ExceptionInterceptor()));
	//return container.CreateServiceProvider(services);

	//private readonly Func<string, IServis> _serviceAccessor;
	//public ValuesController(Func<string, IServis> serviceAccessor)
	//{
	//	_serviceAccessor = serviceAccessor;
	//}

	//[HttpGet]
	//public ActionResult<IEnumerable<string>> Get()
	//{
	//	_serviceAccessor("Servis1").Fonksiyon();
	//	_serviceAccessor("Servis2").Fonksiyon();
	//	return new string[] { "value1", "value2" };
	//}

	//services.AddMvc().AddControllersAsServices();

	//public void ShouldResolveMockedService()
	//{
	//	var builder = new WebHostBuilder()
	//	.UseLightInject()
	//	.ConfigureTestContainer<IServiceContainer>(c => c.RegisterTransient<IFoo, FooMock>())
	//	.UseStartup<TestStartup>();

	//	using (var webHost = builder.Build())
	//	{
	//		var foo = webHost.Services.GetRequiredService<IFoo>();
	//		Assert.IsType<FooMock>(foo);
	//	}
	//}

	//public class FooMock : IFoo { }
}
