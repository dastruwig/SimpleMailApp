using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using Newtonsoft.Json;
using SimpleMailApp.Interfaces;
using SimpleMailApp.Models;
using SimpleMailApp.Models.Endpoint.SendGrid;

namespace SimpleMailApp.Services
{
    public class SendGridEndpoint : IMailEndpoint
    {
        public bool SendMail(SendMessageRequest sendRequest)
        {
            //Todo: Wire up automapper
            // var messageModel = Mapper.Map<SendMessageRequest, MessageModel>(sendRequest);

            var sendUri = WebConfigurationManager.AppSettings["SendGrid_SendUri"];
            var apiKey = WebConfigurationManager.AppSettings["SendGrid_ApiKey"];

            var sendGridRequestData = CreateSendGridRequest(sendRequest);

            WebRequest request = WebRequest.Create(sendUri);

            // Add headers
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.ContentType = "application/json";

            // Serialize body
            // Todo: error handling
            var body = JsonConvert.SerializeObject(sendGridRequestData);

            request.ContentLength = body.Length;
            request.Method = "POST";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Success
                if (response.StatusCode == HttpStatusCode.Accepted)
                    return true;
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    using (var reader = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        // Todo: log error result
                    }
                }
            }

            // Failure
            return false;
        }

        /// <summary>
        /// Maps a SendMessageRequest into a SendGrid MessageModel 
        /// </summary>
        /// <param name="sendRequest"></param>
        /// <returns></returns>
        private MessageModel CreateSendGridRequest(SendMessageRequest sendRequest)
        {
            //Todo: Wire up automapper
            // var messageModel = Mapper.Map<SendMessageRequest, MessageModel>(sendRequest);

            // Read from address from config
            var fromAddress = WebConfigurationManager.AppSettings["FromAddress"];
            var fromAddressName = WebConfigurationManager.AppSettings["FromAddressName"];

            var sendGridRequestData = new MessageModel()
            {
                subject = sendRequest.Subject,
                from = new MailAddressModel()
                {
                    email = fromAddress,
                    name = fromAddressName
                },
                content = new[] {new ContentModel
                {
                    type = "text/html",
                    value = sendRequest.Message
                }},
                personalizations = new[] {new RecipientModel
                {
                    subject = sendRequest.Subject,
                    to = sendRequest.ToAddresses.Select(x => new MailAddressModel()
                    {
                        // Strip out the name
                        name = Regex.Replace(x, "@.+", ""),
                        email = x
                    }).ToArray()
                }},
                reply_to = new MailAddressModel
                {
                    email = fromAddress,
                    name = fromAddress
                },

            };

            if (sendRequest.CcAddresses != null && sendRequest.CcAddresses.Any())
            {
                sendGridRequestData.personalizations[0].cc = sendRequest.CcAddresses.Select(x => new MailAddressModel()
                    {
                        // Strip out the name
                        name = Regex.Replace(x, "@.+", ""),
                        email = x
                    })
                    .ToArray();
            }

            if (sendRequest.BccAddresses != null && sendRequest.BccAddresses.Any())
            {
                sendGridRequestData.personalizations[0].bcc = sendRequest.BccAddresses.Select(x => new MailAddressModel()
                    {
                        // Strip out the name
                        name = Regex.Replace(x, "@.+", ""),
                        email = x
                    })
                    .ToArray();
            }

            return sendGridRequestData;
        }
    }
}