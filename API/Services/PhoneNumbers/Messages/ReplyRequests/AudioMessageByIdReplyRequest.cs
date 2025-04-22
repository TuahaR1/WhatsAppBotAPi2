using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Messages.Requests;

namespace WhatsAppBotAPi.Services.Messages.ReplyRequests
{
    public class AudioMessageByIdReplyRequest : AudioMessageByIdRequest
    {
        [JsonPropertyName("context")]
        public AudioMessageContext Context { get; set; }
    }

    public class AudioMessageContext
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}