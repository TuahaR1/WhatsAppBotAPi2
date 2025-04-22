using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.TwoStepVerification.Requests
{
    public class TwoStepVerificationRequest
    {
        [JsonPropertyName("pin")]
        public string Pin { get; set; }
    }
}
