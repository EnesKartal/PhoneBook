using Microsoft.EntityFrameworkCore;
using Moq;
using PhoneBook.Common.Constants;
using PhoneBook.Report.API.Models.Domain;
using PhoneBook.Report.API.Repositories;

namespace PhoneBook.Report.API.Tests
{
    public class ReportRepositoryTests
    {
        private readonly DbContextOptions<PhoneBookReportDbContext> options;

        public ReportRepositoryTests()
        {
            // DbContextOptions oluşturarak InMemoryDatabase kullanacağız.
            options = new DbContextOptionsBuilder<PhoneBookReportDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddReportToDatabase()
        {
            // Arrange
            var report = new Models.Domain.Entities.Report
            {
                Id = Guid.NewGuid(),
                Location = "Location1",
                RequestDate = DateTime.Now,
                Status = "Status1"
            };

            using (var context = new PhoneBookReportDbContext(options))
            {
                var repository = new ReportRepository(context);

                // Act
                await repository.AddAsync(report);
            }

            // Assert
            using (var context = new PhoneBookReportDbContext(options))
            {
                var result = await context.Report.FirstOrDefaultAsync(p => p.Id == report.Id);
                Assert.NotNull(result);
                Assert.Equal(report.Id, result.Id);
            }
        }
    }
}
