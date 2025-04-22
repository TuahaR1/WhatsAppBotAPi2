using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class TextMessageReplyRequest : TextMessageRequest
    {
        [JsonPropertyName("context")]
        public TextMessageContext Context { get; set; }
    }

    public class TextMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}