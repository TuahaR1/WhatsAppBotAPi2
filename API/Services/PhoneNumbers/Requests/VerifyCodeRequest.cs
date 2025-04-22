using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.PhoneNumbers.Requests
{
    public class VerifyCodeRequest
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
