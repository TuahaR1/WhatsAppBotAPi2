using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class ImageMessageByIdReplyRequest : ImageMessageByIdRequest
    {
        [JsonPropertyName("context")]
        public ImageMessageContext Context { get; set; }
    }

    public class ImageMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}