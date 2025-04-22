using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.SendMessageTemplate;
namespace WhatsAppBotAPi.Controllers.Webhook
{
    [ApiController]
    [Route("webhook")]

    public class WhatsAppWebhookController : ControllerBase
    {
        private readonly ILogger<WhatsAppWebhookController> _logger;
        private readonly IWhatsAppBussinesManager _whatsAppBussinesManager;
        // Replace with your verify token used during webhook setup

        public WhatsAppWebhookController(ILogger<WhatsAppWebhookController> logger, IWhatsAppBussinesManager whatsAppBussinesManager)
        {
            _logger = logger;
            _whatsAppBussinesManager = whatsAppBussinesManager;
        }

        // Webhook verification (GET)
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "hub.mode")] string mode,
                                 [FromQuery(Name = "hub.verify_token")] string token,
                                 [FromQuery(Name = "hub.challenge")] string challenge)
        {
            if (mode == "subscribe" && token == "Apple")
            {
                _logger.LogInformation("WEBHOOK_VERIFIED");
                return Ok(challenge);
            }
            else
            {
                return Forbid();
            }
        }

        // Webhook message handler (POST)
        [HttpPost]
        [HttpPost]
        public IActionResult Post([FromBody] WebhookEvent body)
        {
            _logger.LogInformation("Received webhook: {WebhookBody}", body.ToString());
            try
            {
                var message = body.Entry[0].Changes[0].Value.Messages[0];
                var from = message.From;
                var text = message.Text.Body;

                SendWhatsAppPizzaPayload req = new SendWhatsAppPizzaPayload
                {
                    ItemDate = DateTime.Now,
                    ItemImage = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd",
                    ItemName = "Buger With Cheezs",
                    ItemPrice = 500,
                    TemplateName = "send_prop2",
                    ToNum = from
                };

                _whatsAppBussinesManager.SendFirstTemplateMessageAsync(req);
                _logger.LogInformation($"Incoming message from {from}: {text}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing webhook");
                return BadRequest(ex.Message);
            }
        }
    }
    public class WebhookEvent
    {
        public List<WebhookEntry> Entry { get; set; }
    }

    public class WebhookEntry
    {
        public List<WebhookChange> Changes { get; set; }
    }

    public class WebhookChange
    {
        public WebhookValue Value { get; set; }
    }

    public class WebhookValue
    {
        public List<WebhookMessage> Messages { get; set; }
    }

    public class WebhookMessage
    {
        public string From { get; set; }
        public WebhookMessageText Text { get; set; }
    }

    public class WebhookMessageText
    {
        public string Body { get; set; }
    }


}
