using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class LocationMessageReplyRequest : LocationMessageRequest
    {
        [JsonPropertyName("context")]
        public LocationMessageContext Context { get; set; }
    }

    public class LocationMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}
