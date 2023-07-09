using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using RabbitMQ.Client;
using System.Text;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public CalculatorController()
        {
            var factory = new ConnectionFactory
            {
                HostName = "YOUR_RABBITMQ_HOSTNAME",
                UserName = "YOUR_RABBITMQ_USERNAME",
                Password = "YOUR_RABBITMQ_PASSWORD"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "calculation_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CalculationModel model)
        {
            // Parametreleri MQ'ya bırak
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            _channel.BasicPublish(exchange: "", routingKey: "calculation_queue", basicProperties: null, body: body);

            return Ok();
        }
    }

    public class CalculationModel
    {
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public string Operator { get; set; }
    }
}
