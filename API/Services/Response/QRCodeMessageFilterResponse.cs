using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.Response
{
    public class QRCodeMessageFilterResponse
    {
        [JsonPropertyName("data")]
        public List<QRCodeMessageResponse> Data { get; set; }
    }
}
