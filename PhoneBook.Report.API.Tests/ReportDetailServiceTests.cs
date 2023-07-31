using Moq;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Report.API.Models.Domain.Entities;
using PhoneBook.Report.API.Repositories;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Tests
{
    public class ReportDetailServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldReturnReportDetail()
        {
            // Arrange
            var response = new ReportResponseModel
            {
                Id = Guid.NewGuid(),
                PeopleCount = 10,
                PhoneNumberCount = 20
                // Diğer properties ekleyebilirsiniz.
            };

            var mockRepository = new Mock<IReportDetailRepository>();
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<ReportDetail>()))
                .ReturnsAsync((ReportDetail record) => record);

            var service = new ReportDetailService(mockRepository.Object);

            // Act
            var result = await service.AddAsync(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.PeopleCount, result.PeopleCount);
            Assert.Equal(response.PhoneNumberCount, result.PhoneNumberCount);
            // Diğer properties için de assert ekleyebilirsiniz.
        }

        [Fact]
        public async Task GetByReportIdAsync_ShouldReturnGetReportDetailDTO()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var entity = new ReportDetail
            {
                Id = Guid.NewGuid(),
                PeopleCount = 10,
                PhoneNumberCount = 20,
                ReportId = reportId,
                Report = new Models.Domain.Entities.Report
                {
                    Location = "Location1",
                    RequestDate = DateTime.Now,
                    Status = "Preparing",
                    Id = reportId,
                }
            };

            var mockRepository = new Mock<IReportDetailRepository>();
            mockRepository.Setup(repo => repo.GetByReportIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => entity);

            var service = new ReportDetailService(mockRepository.Object);

            // Act
            var result = await service.GetByReportIdAsync(reportId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.PeopleCount, result.PeopleCount);
            Assert.Equal(entity.PhoneNumberCount, result.PhoneNumberCount);
            Assert.Equal(entity.ReportId, result.ReportId);
        }

    }
}