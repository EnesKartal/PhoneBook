namespace PhoneBook.Common.Interfaces
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
