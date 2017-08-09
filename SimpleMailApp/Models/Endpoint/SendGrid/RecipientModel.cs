namespace SimpleMailApp.Models.Endpoint.SendGrid
{
    public class RecipientModel
    {
        public MailAddressModel[] to { get; set; }
        public MailAddressModel[] cc { get; set; }
        public MailAddressModel[] bcc { get; set; }
        public string subject { get; set; }
    }
}