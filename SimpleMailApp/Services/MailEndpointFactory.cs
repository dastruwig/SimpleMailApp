using System.Web.Http;
using Autofac;
using Autofac.Core;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;

namespace SimpleMailApp.Services
{
    public class MailEndpointFactory : IMailEndpointFactory
    {
        private readonly ILifetimeScope _scope;

        public MailEndpointFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }


        /// <summary>
        /// Creates an IMailEndpoint for the given EndpointIdentifier
        /// </summary>
        public IMailEndpoint CreateEndpoint(EndpointIdentifier endpointIdentifier)
        {
            return _scope.ResolveNamed<IMailEndpoint>(endpointIdentifier.ToString());
        }
    }
}