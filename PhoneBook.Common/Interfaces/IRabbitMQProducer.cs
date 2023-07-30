namespace PhoneBook.Common.Interfaces
{
    public interface IRabbitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}
