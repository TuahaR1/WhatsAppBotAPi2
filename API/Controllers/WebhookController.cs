using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WhatsAppBotAPi.Services.Configurations;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.SendMessageTemplate;

namespace WhatsAppBotAPi.Controllers.Webhook
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        private readonly IConfiguration _configuration;
        private readonly IWhatsAppBussinesManager _whatsAppBusinessManager;
        private readonly WhatsAppConfig _waConfig;
        public WebhookController(IConfiguration configuration, IWhatsAppBussinesManager _whatsAppBusinessManager)
        {
            _configuration = configuration;
            _whatsAppBusinessManager = _whatsAppBusinessManager;
            _waConfig = new WhatsAppConfig();
            _configuration.GetSection("WhatsApp").Bind(_waConfig);

            // Setup the HttpClient headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _waConfig.AccessToken);
            client
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult GetWebHook(
  [FromQuery(Name = "hub.mode")] string mode,
  [FromQuery(Name = "hub.verify_token")] string token,
  [FromQuery(Name = "hub.challenge")] string challenge)
        {
            string VERIFY_TOKEN = _waConfig.MyAccessToken;  //"apple"; // Replace with your own token

            if (mode == "subscribe" && token == VERIFY_TOKEN)
            {
                return Ok(challenge); // Facebook will accept this
            }

            return Forbid(); // 403 if token mismatch
        }



        private string GetUrl()
        {
            // Example: https://graph.facebook.com/v13.0/10938439232/messages
            return $"{_waConfig.BaseUrl}/{_waConfig.Version}/{_waConfig.AppID}/messages";
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveMessage([FromBody] dynamic incomingMessage)
        {
            // Extract message data
            string from = incomingMessage.entry[0].changes[0].value.messages[0].from;
            string messageText = incomingMessage.entry[0].changes[0].value.messages[0].text?.body;

            if (!string.IsNullOrEmpty(messageText) && messageText.Trim().ToLower() == "hi")
            {
                // Call your method to send a template
                await _whatsAppBusinessManager.SendFirstTemplateMessageAsync(new SendWhatsAppPizzaPayload
                {
                    ToNum = from,
                    TemplateName = "send_catalog_2", // or whatever your template is
                    ItemName = "Pizza",
                    ItemPrice = 500,
                    ItemDate = DateTime.UtcNow,
                    ItemImage = "https://your-image-link.jpg"
                });
            }

            return Ok();
        }

    }
}
