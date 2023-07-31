using Newtonsoft.Json;
using PhoneBook.Common.Constants;
using PhoneBook.Common.Interfaces;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using RabbitMQ.Client;
using System.Text;

namespace PhoneBook.Contact.API.RabbitMQ
{
    public class PhoneBookRabbitMQProducer : IRabbitMQProducer
    {
        private readonly IServiceScopeFactory scopeFactory;
        public PhoneBookRabbitMQProducer(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public void SendMessage<T>(T message)
        {
            var scope = scopeFactory.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var rabbitMQConfig = config.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.Host,
                Port = rabbitMQConfig.Port,
                UserName = rabbitMQConfig.Username,
                Password = rabbitMQConfig.Password
            };

            var connection = factory.CreateConnection();
            
            using
            var channel = connection.CreateModel();
            
            channel.QueueDeclare(RabbitMQConstants.REPORT_QUEUE, exclusive: false, autoDelete: false);
            
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish(exchange: "", routingKey: RabbitMQConstants.REPORT_QUEUE, body: body);
        }
    }
}
