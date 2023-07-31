using Newtonsoft.Json;
using PhoneBook.Common.Constants;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Contact.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PhoneBook.Contact.API.RabbitMQ
{
    public class PhoneBookRabbitMQConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IContactService contactService;

        public PhoneBookRabbitMQConsumer(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();
            contactService = scope.ServiceProvider.GetRequiredService<IContactService>();

            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var rabbitMQConfig = config.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.Host,
                Port = rabbitMQConfig.Port,
                UserName = rabbitMQConfig.Username,
                Password = rabbitMQConfig.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: RabbitMQConstants.PREPARE_REPORT_QUEUE,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                ReportRequestModel? response = JsonConvert.DeserializeObject<ReportRequestModel>(message);
                await contactService.PrepareReportAsync(response);

                Console.WriteLine("Received Message: " + message);
            };

            _channel.BasicConsume(queue: RabbitMQConstants.PREPARE_REPORT_QUEUE,
                                  autoAck: true,
                                  consumer: consumer);

            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}