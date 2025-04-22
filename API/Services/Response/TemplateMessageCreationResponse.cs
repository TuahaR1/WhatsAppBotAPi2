using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.Response
{
	public class TemplateMessageCreationResponse
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonPropertyName("category")]
		public string Category { get; set; }
	}
}
