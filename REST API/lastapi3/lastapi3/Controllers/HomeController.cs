using lastapi3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace lastapi3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueueClient _queueClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            string connectionString = "Endpoint=sb://calculator-servicebus.servicebus.windows.net/" +
                ";SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=iogSf" +
                "/xR6uesY6Ca7amEVRAX9h24W4ZTl+ASbPXCTcY=";
            string queueName = "calculatormq";
            _queueClient = new QueueClient(connectionString, queueName);
        }



    public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(int num1, int num2, string operation)
        {
            decimal result;
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    return BadRequest("Geçersiz işlem");
            }

            // Hesap sonucunu Service Bus kuyruğuna gönderme
            string messageToSend = result.ToString();
            _ = SendMessage(messageToSend);

            return Ok(result);
        }

        [HttpPost]
        public async Task SendMessage(string message)
        {
            var encodedMessage = new Message(Encoding.UTF8.GetBytes(message));
            await _queueClient.SendAsync(encodedMessage);
        }

        //[HttpGet]
        //public async Task<IActionResult> ReceiveMessage()
        //{
        //    var receivedMessage = await _queueClient.ReceiveAsync();
        //    if (receivedMessage != null)
        //    {
        //        var messageBody = Encoding.UTF8.GetString(receivedMessage.Body);
        //        await _queueClient.CompleteAsync(receivedMessage.SystemProperties.LockToken);
        //        return Ok(messageBody);
        //    }
        //    return NoContent();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
