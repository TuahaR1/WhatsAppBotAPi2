using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.Messages.Requests
{
    public class ConversationalComponentRequest
    {
        [JsonPropertyName("enable_welcome_message")]
        public bool EnableWelcomeMessage { get; set; }

        [JsonPropertyName("commands")]
        public List<ConversationalComponentCommand> Commands { get; set; }

        [JsonPropertyName("prompts")]
        public List<string> Prompts { get; set; }
    }

    public class ConversationalComponentCommand
    {
        [JsonPropertyName("command_name")]
        public string CommandName { get; set; }

        [JsonPropertyName("command_description")]
        public string CommandDescription { get; set; }
    }
}
