using WhatsAppBotAPi.Services.Webhook;

namespace WhatsAppBotAPi.Services.Interfaces;
public interface IGenericStatus
{

    public string Id { get; set; }

    public string Status { get; set; }

    public string Timestamp { get; set; }

    public string RecipientId { get; set; }

    public Conversation Conversation { get; set; }


    public Pricing Pricing { get; set; }
}