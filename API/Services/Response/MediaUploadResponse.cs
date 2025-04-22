using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.Response
{
    public class MediaUploadResponse
    {
        [JsonPropertyName("id")]
        public string MediaId { get; set; }
    }
}
