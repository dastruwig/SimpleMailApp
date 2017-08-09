using System;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;

namespace SimpleMailApp.Services
{
    public class MailGunEndpoint : IMailEndpoint
    {
        public bool SendMail(SendMessageRequest message)
        {
            throw new NotImplementedException();
        }
    }
}