using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppBotAPi.Services.Response;
using WhatsAppBotAPi.Services.SendMessageTemplate;

namespace WhatsAppBotAPi.Services.Interfaces
{
    public interface IWhatsAppBussinesManager
    {
        Task<WhatsAppResponse> SendFirstTemplateMessageAsync(SendWhatsAppPizzaPayload payload);
        Task<WhatsAppResponse> SendFirstTemplateMessageAsync(SendWhatsAppPayload2 payload);

    }
}
