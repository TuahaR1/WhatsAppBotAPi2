using System.Collections.Generic;

namespace WhatsAppBotAPi.Services.Response
{
	public class TemplateByNameResponse : TemplateBaseResponse
	{
        public Dictionary<string, object> AdditionalFields { get; set; }
    }
}
