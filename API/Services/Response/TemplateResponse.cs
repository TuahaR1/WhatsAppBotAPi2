using System.Collections.Generic;

namespace WhatsAppBotAPi.Services.Response
{
    public class TemplateResponse : TemplateBaseResponse
    {
        public Dictionary<string, object> AdditionalFields { get; set; }
    }
}
