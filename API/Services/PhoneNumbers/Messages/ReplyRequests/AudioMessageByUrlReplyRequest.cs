using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class AudioMessageByUrlReplyRequest : AudioMessageByUrlRequest
    {
        [JsonPropertyName("context")]
        public AudioMessageUrlContext Context { get; set; }
    }

    public class AudioMessageUrlContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}