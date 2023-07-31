using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Report.API.Controllers;
using PhoneBook.Report.API.Models.DTO.ReportDetail.GetReportDetail;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Tests
{
    public class ReportDetailControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnOkResult_WithValidReportId()
        {
          // Arrange
            var mockReportDetailService = new Mock<IReportDetailService>();
            var reportId = Guid.NewGuid();

            var expectedData = new GetReportDetailDTO
            {
                ReportId = reportId,
            };

            mockReportDetailService.Setup(service => service.GetByReportIdAsync(reportId)).ReturnsAsync(expectedData);
            var controller = new ReportDetailController(mockReportDetailService.Object);

            // Act
            var result = await controller.Get(reportId.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var data = okResult.Value as GetReportDetailDTO;
            Assert.Equal(expectedData, data);
        }

        [Fact]
        public async Task Get_ShouldReturnBadRequest_WithInvalidReportId()
        {
            // Arrange
            var mockReportDetailService = new Mock<IReportDetailService>();
            var controller = new ReportDetailController(mockReportDetailService.Object);
            var invalidReportId = "invalid-id";

            // Act
            var result = await controller.Get(invalidReportId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
