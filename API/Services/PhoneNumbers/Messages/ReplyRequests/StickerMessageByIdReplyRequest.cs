using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class StickerMessageByIdReplyRequest : StickerMessageByIdRequest
    {
        [JsonPropertyName("context")]
        public StickerMessageContext Context { get; set; }
    }

    public class StickerMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}