using Microsoft.AspNetCore.Mvc;

namespace WhatsAppBotAPi.Controllers.Ping
{
    [ApiController]
    [Route("ping")]
    public class PingController : ControllerBase
    {
        public PingController()
        {
        }

        [HttpGet]
        public object Get()
        {
            return Ok("API is live and responsive!");
        }
    }
}
