using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class ImageMessageByUrlReplyRequest : ImageMessageByUrlRequest
    {
        [JsonPropertyName("context")]
        public ImageMessageUrlContext Context { get; set; }
    }

    public class ImageMessageUrlContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}