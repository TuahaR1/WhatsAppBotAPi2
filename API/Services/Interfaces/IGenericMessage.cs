using WhatsAppBotAPi.Services.Webhook;

namespace WhatsAppBotAPi.Services.Interfaces;
public interface IGenericMessage
{
    string From { get; set; }
    string Id { get; set; }
    string Timestamp { get; set; }
    string Type { get; set; }

    public MessageContext? Context { get; set; }
}