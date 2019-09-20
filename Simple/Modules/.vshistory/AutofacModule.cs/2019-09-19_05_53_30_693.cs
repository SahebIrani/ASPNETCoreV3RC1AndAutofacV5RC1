
using Autofac;

using Simple.Services;

namespace TaghelperWorkerServiceModelBinderFluentValidationAjax.Modules
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
            => builder.RegisterType<IPrintMessages>().SingleInstance().PropertiesAutowired();
    }
}
