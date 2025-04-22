using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class DocumentMessageByUrlReplyRequest : DocumentMessageByUrlRequest
    {
        [JsonPropertyName("context")]
        public DocumentMessageUrlContext Context { get; set; }
    }

    public class DocumentMessageUrlContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}