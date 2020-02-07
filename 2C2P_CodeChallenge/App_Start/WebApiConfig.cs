using Autofac;
using System.Web.Http;
using _2C2P.Services;
using Autofac.Integration.WebApi;
using System.Reflection;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace _2C2P_CodeChallenge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // AutoFac
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TransactionService>().As<ITransactionService>();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
