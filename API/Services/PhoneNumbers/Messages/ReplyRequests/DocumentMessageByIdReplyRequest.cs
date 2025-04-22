using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class DocumentMessageByIdReplyRequest : DocumentMessageByIdRequest
    {
        [JsonPropertyName("context")]
        public DocumentMessageContext Context { get; set; }
    }

    public class DocumentMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}