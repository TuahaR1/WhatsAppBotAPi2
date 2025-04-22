using System.Collections.Generic;

namespace WhatsAppBotAPi.Services.ViewModel
{
    public class SendMediaMessageViewModel
    {
        public string RecipientPhoneNumber { get; set; }
        public List<string> MediaType { get; set; }
        public string SelectedMediaType { get; set; }
        public string Message { get; set; }
        public string? MediaLink { get; set; }
        public string? MediaId { get; set; }
    }
}
