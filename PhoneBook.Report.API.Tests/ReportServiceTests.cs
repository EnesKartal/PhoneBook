using Moq;
using PhoneBook.Common.Interfaces;
using PhoneBook.Report.API.Repositories;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Tests
{
    public class ReportServiceTests
    {
        private Mock<IReportRepository> mockReportRepository;
        private Mock<IReportDetailService> mockReportDetailService;
        private Mock<IRabbitMQProducer> mockRabbitMQProducer;
        private ReportService reportService;

        public ReportServiceTests()
        {
            mockReportRepository = new Mock<IReportRepository>();
            mockReportDetailService = new Mock<IReportDetailService>();
            mockRabbitMQProducer = new Mock<IRabbitMQProducer>();
            reportService = new ReportService(mockReportRepository.Object, mockReportDetailService.Object, mockRabbitMQProducer.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfReports()
        {
            // Arrange
            var expectedReports = new List<Models.Domain.Entities.Report>
            {
                new Models.Domain.Entities.Report { Id = Guid.NewGuid(), Location = "Location1", RequestDate = DateTime.Now, Status = "Status1" },
                new Models.Domain.Entities.Report{ Id = Guid.NewGuid(), Location = "Location2", RequestDate = DateTime.Now, Status = "Status2" }
            };

            mockReportRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedReports);

            // Act
            var result = await reportService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReports.Count, result.Count());
            // Diğer gerekli assert işlemlerini burada yapabilirsiniz.
        }

        // Diğer test metotlarını buraya ekleyebilirsiniz.
    }
}
