using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class VideoMessageByUrlReplyRequest : VideoMessageByUrlRequest
    {
        [JsonPropertyName("context")]
        public VideoMessageUrlContext Context { get; set; }
    }

    public class VideoMessageUrlContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}