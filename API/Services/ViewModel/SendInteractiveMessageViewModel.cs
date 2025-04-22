using System.Collections.Generic;

namespace WhatsAppBotAPi.Services.ViewModel
{
    public class SendInteractiveMessageViewModel
    {
        public string RecipientPhoneNumber { get; set; }
        public List<string> InteractiveType { get; set; }
        public string SelectedInteractiveType { get; set; }
        public string Message { get; set;}
    }
}
