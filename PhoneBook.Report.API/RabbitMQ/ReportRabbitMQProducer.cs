using Newtonsoft.Json;
using PhoneBook.Common.Constants;
using PhoneBook.Common.Interfaces;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using RabbitMQ.Client;
using System.Text;

namespace PhoneBook.Report.API.RabbitMQ
{
    public class ReportRabbitMQProducer : IRabbitMQProducer
    {
        private readonly IServiceScopeFactory scopeFactory;
        public ReportRabbitMQProducer(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public void SendMessage<T>(T message)
        {
            var scope = scopeFactory.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var rabbitMQConfig = config.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.Host,
                Port = rabbitMQConfig.Port,
                UserName = rabbitMQConfig.Username,
                Password = rabbitMQConfig.Password
            };

            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(RabbitMQConstants.PREPARE_REPORT_QUEUE, exclusive: false, autoDelete: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: RabbitMQConstants.PREPARE_REPORT_QUEUE, body: body);
        }
    }
}