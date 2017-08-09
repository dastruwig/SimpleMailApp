using SimpleMailApp.Models;
using SimpleMailApp.Services;

namespace SimpleMailApp.Interfaces
{
    public interface IMailEndpointFactory
    {
        IMailEndpoint CreateEndpoint(EndpointIdentifier endpointIdentifier);
    }
}