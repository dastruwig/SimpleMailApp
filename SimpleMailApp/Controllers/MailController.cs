using System;
using System.Web.Http;
using AutoMapper;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;
using SimpleMailApp.Services;

namespace SimpleMailApp.Controllers
{
    /// <summary>
    /// Mail API controller
    /// </summary>
    public class MailController : ApiController
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Sends an email message to the requested mail endpoint
        /// </summary>
        public IHttpActionResult Post(SendMessageRequest requestModel)
        {
            // Validate model
            // Todo: decorate model class members with validation attributes (i.e. [Required], [NotNull], etc.)
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // Locate the relevant service
                var serviceIdentifier =
                    (EndpointIdentifier) Enum.Parse(typeof(EndpointIdentifier), requestModel.Service);

                // Send the mail
                if (_mailService.SendMail(requestModel, serviceIdentifier))
                    return Ok();

                // Just return a 500 response for now
                // Todo: have more specific error messages
                return InternalServerError();
            }
            catch (Exception ex)
            {
                // Todo: add logging
                return InternalServerError();
            }

        }
    }
}
