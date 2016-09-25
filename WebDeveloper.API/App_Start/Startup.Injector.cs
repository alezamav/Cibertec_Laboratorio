using LightInject;
using System;
using System.Reflection;
using System.Web.Http;

namespace WebDeveloper.API
{
    public partial class Startup
    {

        private void ConfigureDependency()
        {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("WebDeveloper.Model*.dll");
            container.RegisterAssembly("WebDeveloper.Repository*.dll");
            container.RegisterApiControllers();
            container.EnableWebApi(GlobalConfiguration.Configuration);
        }

    }
}