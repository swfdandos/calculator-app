using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YourController : ControllerBase
    {
        private readonly ILogger<YourController> _logger;

        public YourController(ILogger<YourController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] YourModel model)
        {
            // Parametreleri işleyin ve mesaj kuyruğuna gönderin
            // Örnek olarak RabbitMQ kullanıyorsanız:
            // RabbitMQ'ya bağlanın ve mesajı yayınlayın

            _logger.LogInformation("Parametreler alındı ve mesaj kuyruğuna gönderildi.");
            return Ok();
        }
    }
}
