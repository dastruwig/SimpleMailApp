

using SimpleMailApp.Models;

namespace SimpleMailApp.Interfaces
{
    public interface IMailEndpoint
    {
        bool SendMail(SendMessageRequest message);
    }
}