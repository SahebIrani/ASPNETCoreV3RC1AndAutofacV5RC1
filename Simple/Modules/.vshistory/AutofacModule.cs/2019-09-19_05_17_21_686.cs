
using Autofac;

using Simple.Services;

namespace TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IPrintMessages>().PropertiesAutowired();

            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the services.AddAutofac() that happens in Program and registers Autofac
            // as the service provider.
            //builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
            //	.As<IValuesService>()
            //	.InstancePerLifetimeScope();


            //var builder = new ContainerBuilder();

            //builder.Register(x => new MyCustomGlobalActionFilter())
            //.AsWebApiActionFilterOverrideFor<MyCustomController>()
            //.InstancePerRequest()
            //.PropertiesAutowired();

            //var propSelector = new DefaultPropertySelector();
            //builder
            //  .RegisterAssemblyTypes(typeof(Controller).Assembly)
            //  .AssignableTo<Controller>()
            //  .InstancePerLifetimeScope()
            //  .PropertiesAutowired(propSelector);

            //builder.Populate(services);
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //	.Where(t => typeof(Controller).IsAssignableFrom(t))
            //	.PropertiesAutowired();
            //builder.RegisterAssemblyTypes(typeof(IAutoWire).Assembly)
            //	.Where(t => typeof(IAutoWire).IsAssignableFrom(t))
            //	.PropertiesAutowired()
            //	.AsImplementedInterfaces()
            //	.InstancePerLifetimeScope();

            //var builder = new ContainerBuilder();
            //var manager = new ApplicationPartManager();

            //manager.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly()));
            //manager.FeatureProviders.Add(new ControllerFeatureProvider());

            //var feature = new ControllerFeature();

            //manager.PopulateFeature(feature);

            //builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            //builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();

            //builder.RegisterAssemblyTypes(typeof(ITestRepository).Assembly)
            //	.Where(t => typeof(ITestRepository).IsAssignableFrom(t))
            //	.PropertiesAutowired()
            //	.AsImplementedInterfaces()
            //	.InstancePerLifetimeScope();

            //builder.Populate(services);

            //IContainer container = builder.Build();
            //return container.Resolve<IServiceProvider>();



            //var assemblyProvider = new StaticAssemblyProvider();
            //assemblyProvider.CandidateAssemblies.Add(typeof(ValuesController).GetTypeInfo().Assembly);
            //var controllerTypeProvider = new DefaultControllerTypeProvider(assemblyProvider);
            //var controllerTypes = controllerTypeProvider.ControllerTypes.Select(t => Type.GetType(t.FullName)).ToArray();

            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            //services.Replace(ServiceDescriptor.Singleton<IControllerTypeProvider>(provider => controllerTypeProvider));
            //services.Replace(ServiceDescriptor.Singleton<IAssemblyProvider>(provider => assemblyProvider));

            //services.AddMvc();

            //var builder = new ContainerBuilder();
            //builder.RegisterType<FooService>().As<IFooService>();
            //builder.RegisterTypes(controllerTypes).PropertiesAutowired();

            //builder.Populate(services);
            //var container = builder.Build();
            //return container.Resolve<IServiceProvider>();



            //services.Replace(ServiceDescriptor.Transient < IControllerActivator, ServiceBasedControllerActivator = "" > ());

            //var builder = new ContainerBuilder();

            //var manager = new ApplicationPartManager();

            //manager.ApplicationParts.Add(new AssemblyPart(/* assembly with controllers, usually just Assembly.GetExecutingAssembly() */ ));

            //manager.FeatureProviders.Add(new ControllerFeatureProvider());

            //var feature = new ControllerFeature();

            //manager.PopulateFeature(feature);

            //builder.RegisterType<applicationpartmanager>().AsSelf().SingleInstance(); // not sure if actually needed any more

            //builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();

            //builder.Populate(services);


            //// register your services against Autofac here
            //builder.RegisterType<FooService>().As<IFooService>();

            //// find and register all controllers from the current assembly
            //// and autowire their properties
            //builder.RegisterAssemblyTypes(typeof(ValuesController).GetTypeInfo().Assembly)
            //	.Where(
            //		t =>
            //			typeof(Controller).IsAssignableFrom(t) &&
            //			t.Name.EndsWith("Controller", StringComparison.Ordinal)).PropertiesAutowired();


            //// When you do service population, it will include your controller
            //// types automatically.
            //builder.Populate(services);
            //// If you want to set up a controller for, say, property injection
            //// you can override the controller registration after populating services.
            //builder.RegisterType<MyController>().PropertiesAutowired();
            //this.ApplicationContainer = builder.Build();
            //return new AutofacServiceProvider(this.ApplicationContainer);




            //ContainerBuilder builder = new ContainerBuilder();

            //builder.Populate(services);//Autofac.Extensions.DependencyInjection

            //builder.RegisterType<TestService>().As<ITestService>().PropertiesAutowired();
            //builder.RegisterType<TestRepository>().As<ITestRepository>().InstancePerLifetimeScope();

            //Type[] controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            //    .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

            //builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

            //ITestService testService = builder.Build().Resolve<ITestService>();

            //string res = testService.PrintTest("SinjulNSBH");

            //builder.RegisterType<MyServiceX>().PropertiesAutowired((new InjectPropertySelector(true)));

            //var container = builder.Build();
            //HomeController test2 = container.Resolve<HomeController>();
            //return new AutofacServiceProvider(container);

            //ApplicationContainer = container;
            //return new AutofacServiceProvider(ApplicationContainer);


            //https://autofaccn.readthedocs.io/en/latest/register/prop-method-injection.html
            //Property Injection
            //builder.Register(c => new A { B = c.Resolve<B>() });
            //builder.Register(c => new A()).OnActivated(e => e.Instance.B = e.Context.Resolve<B>());
            //builder.RegisterType<A>().PropertiesAutowired();
            //builder.RegisterType<A>().WithProperty("PropertyName", propertyValue);
            ////Method Injection
            //builder
            //  .Registe<TestService>()
            //  .OnActivating(e => {
            //	  var dep = e.Context.Resolve<ITestService>();
            //	  e.Instance.SetTheDependency(dep);
            //  });

        }
    }

    //public class MyServiceX
    //{
    //	[Inject]
    //	public ITestRepository TestRepository { get; set; }
    //	[Inject]
    //	public ITestService TestService { get; set; }
    //}

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class InjectAttribute : Attribute
    //{
    //	public InjectAttribute() : base() { }
    //}
    //public class InjectPropertySelector : DefaultPropertySelector
    //{
    //	public InjectPropertySelector(bool preserveSetValues) : base(preserveSetValues)
    //	{ }

    //	public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
    //	{
    //		var attr = propertyInfo.GetCustomAttribute<InjectAttribute>(inherit: true);
    //		return attr != null && propertyInfo.CanWrite
    //				&& (!PreserveSetValues
    //				|| (propertyInfo.CanRead && propertyInfo.GetValue(instance, null) == null));
    //	}
    //}
}
