using PhoneBook.Common.Models.Extra.RabbitMQ;

namespace PhoneBook.Common.Tests
{
    public class RabbitMQConfigTests
    {
        [Fact]
        public void CanCreateRabbitMQConfig()
        {
            // Arrange & Act
            var config = new RabbitMQConfig();

            // Assert
            Assert.NotNull(config);
            Assert.Null(config.Host);
            Assert.Equal(0, config.Port);
            Assert.Null(config.Username);
            Assert.Null(config.Password);
        }

        [Fact]
        public void CanSetAndGetProperties()
        {
            // Arrange and Act
            var config = new RabbitMQConfig()
            {
                Host = "localhost",
                Port = 5672,
                Username = "guest",
                Password = "guest"
            };

            // Assert
            Assert.Equal("localhost", config.Host);
            Assert.Equal(5672, config.Port);
            Assert.Equal("guest", config.Username);
            Assert.Equal("guest", config.Password);
        }
    }
}