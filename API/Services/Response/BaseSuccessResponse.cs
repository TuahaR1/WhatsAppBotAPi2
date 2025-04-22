using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.Response
{
    public class BaseSuccessResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
