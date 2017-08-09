using SimpleMailApp.Services;

namespace SimpleMailApp.Models.Endpoint.SendGrid
{
    public class MessageModel
    {
        public RecipientModel[] personalizations { get; set; }
        public MailAddressModel from { get; set; }
        public MailAddressModel reply_to { get; set; }
        public string subject { get; set; }
        public ContentModel[] content { get; set; }

    }
}