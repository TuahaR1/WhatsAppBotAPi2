using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class ContactMessageReplyRequest : ContactMessageRequest
    {
        [JsonPropertyName("context")]
        public ContactMessageContext Context { get; set; }
    }

    public class ContactMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}