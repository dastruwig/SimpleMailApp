using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;

namespace SimpleMailApp.Services
{
    /// <summary>
    /// Mail domain service
    /// </summary>
    public class MailService : IMailService
    {
        readonly IMailEndpointFactory _mailEndpointFactory;

        public MailService(IMailEndpointFactory mailEndpointFactory)
        {
            _mailEndpointFactory = mailEndpointFactory;
        }

        /// <summary>
        /// Creates an appropriate mail endpoint and forwards the SendMessageRequest to it
        /// for processing
        /// </summary>
        /// <returns></returns>
        public bool SendMail(SendMessageRequest message, EndpointIdentifier serviceIdentifier)
        {
            var mailEndpoint = _mailEndpointFactory.CreateEndpoint(serviceIdentifier);
            return mailEndpoint.SendMail(message);
        }

    }
}