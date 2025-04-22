using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class ListMessageReplyRequest : InteractiveListMessageRequest
    {
        [JsonPropertyName("context")]
        public ListMessageContext Context { get; set; }
    }

    public class ListMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}
