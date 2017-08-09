using SimpleMailApp.Models;
using SimpleMailApp.Services;

namespace SimpleMailApp.Interfaces
{
    public interface IMailService
    {
        bool SendMail(SendMessageRequest request, EndpointIdentifier serviceIdentifier);
        

    }
}