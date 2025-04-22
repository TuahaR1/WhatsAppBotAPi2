using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class VideoMessageByIdReplyRequest : VideoMessageByIdRequest
    {
        [JsonPropertyName("context")]
        public VideoMessageContext Context { get; set; }
    }

    public class VideoMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}