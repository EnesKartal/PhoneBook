using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Common.Constants;
using PhoneBook.Report.API.Controllers;
using PhoneBook.Report.API.Models.DTO.Report.GetReport;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Tests
{
    public class ReportControllerTests
    {
        private Mock<IReportService> mockReportService;
        private ReportController reportController;

        public ReportControllerTests()
        {
            mockReportService = new Mock<IReportService>();

            reportController = new ReportController(mockReportService.Object);
        }

        [Fact]
        public async Task GetAll_Should_Return_OkResult_With_ReportList()
        {
            // Arrange
            var mockReports = new List<GetReportResponseDTO>
            {
                new GetReportResponseDTO { Id = Guid.NewGuid(),Location="Istanbul", RequestDate= new DateTime(), Status= ReportConstants.PREPARING },
                new GetReportResponseDTO { Id = Guid.NewGuid(),Location="Ankara", RequestDate= new DateTime().AddHours(1), Status= ReportConstants.COMPLETED },
            };
            mockReportService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockReports);

            // Act
            var result = await reportController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reports = Assert.IsAssignableFrom<IEnumerable<GetReportResponseDTO>>(okResult.Value);
            Assert.Equal(2, reports.Count()); // Burada listenin iki elemanlý olduðunu test edebilirsiniz
        }

        [Fact]
        public async Task RequestReport_Should_Return_OkResult_When_Called()
        {
            // Arrange
            string location = "TestLocation";

            // Act
            var result = await reportController.RequestReport(location);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}