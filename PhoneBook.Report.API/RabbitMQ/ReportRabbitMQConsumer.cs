using PhoneBook.Common.Constants;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Report.API.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace PhoneBook.Report.API.RabbitMQ
{
    public class ReportRabbitMQConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IReportService reportService;

        public ReportRabbitMQConsumer(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();
            this.reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

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

            _channel.QueueDeclare(queue: RabbitMQConstants.REPORT_QUEUE,
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

                ReportResponseModel? response = JsonConvert.DeserializeObject<ReportResponseModel>(message);
                await reportService.ReportCompleteActionAsync(response);
            };

            _channel.BasicConsume(queue: RabbitMQConstants.REPORT_QUEUE,
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
