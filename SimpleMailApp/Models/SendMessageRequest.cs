using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleMailApp.Services;

namespace SimpleMailApp.Models
{
    public class SendMessageRequest
    {
        public string Service { get; set; }
        public string[] ToAddresses { get; set; }
        public string[] CcAddresses { get; set; }
        public string[] BccAddresses { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

   
}