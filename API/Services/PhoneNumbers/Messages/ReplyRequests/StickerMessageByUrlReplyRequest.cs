using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class StickerMessageByUrlReplyRequest : StickerMessageByUrlRequest
    {
        [JsonPropertyName("context")]
        public StickerMessageUrlContext Context { get; set; }
    }

    public class StickerMessageUrlContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}