using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace WhatsAppBotAPi.Controllers.Webhook
{
    [ApiController]
    [Route("webhook")]

    public class WhatsAppWebhookController : ControllerBase
    {
        private readonly ILogger<WhatsAppWebhookController> _logger;
        private readonly IConfiguration _configuration;
        // Replace with your verify token used during webhook setup

        public WhatsAppWebhookController(ILogger<WhatsAppWebhookController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
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
        public IActionResult Post([FromBody] JsonElement body)
        {
            _logger.LogInformation("Received webhook: {WebhookBody}", body.ToString());

            // You can parse the JSON manually or create a model based on WhatsApp Webhook structure
            // Here's an example of checking for a new message:
            var entry = body.GetProperty("entry")[0];
            var changes = entry.GetProperty("changes")[0];
            var value = changes.GetProperty("value");

            if (value.TryGetProperty("messages", out var messages))
            {
                var message = messages[0];
                var from = message.GetProperty("from").GetString();
                var text = message.GetProperty("text").GetProperty("body").GetString();

                _logger.LogInformation($"Incoming message from {from}: {text}");

                // You can respond to this message using WhatsApp Cloud API
            }

            return Ok();
        }
    }

}
