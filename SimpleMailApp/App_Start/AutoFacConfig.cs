using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;
using SimpleMailApp.Services;

namespace SimpleMailApp
{
    public class AutoFacConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container); ;
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MailEndpointFactory>()
                .As<IMailEndpointFactory>()
                .SingleInstance();

            builder.RegisterType<SendGridEndpoint>()
                .Named<IMailEndpoint>(EndpointIdentifier.SendGrid.ToString());

            builder.RegisterType<MailGunEndpoint>()
                .Named<IMailEndpoint>(EndpointIdentifier.MailGun.ToString());

            builder.RegisterType<Mapper>()
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<MailService>()
                .As<IMailService>()
                .SingleInstance();

            Container = builder.Build();

            return Container;
        }

    }
}