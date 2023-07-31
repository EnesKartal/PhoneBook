using Microsoft.EntityFrameworkCore;
using PhoneBook.Report.API.Models.Domain;
using PhoneBook.Report.API.Models.Domain.Entities;

namespace PhoneBook.Report.API.Repositories
{
    public class ReportDetailRepository : IReportDetailRepository
    {
        private readonly PhoneBookReportDbContext dbContext;
        public ReportDetailRepository(PhoneBookReportDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ReportDetail> AddAsync(ReportDetail reportDetail)
        {
            reportDetail.Id = Guid.NewGuid();
            await dbContext.ReportDetail.AddAsync(reportDetail);
            await dbContext.SaveChangesAsync();
            return reportDetail;
        }

        public async Task<ReportDetail> GetByReportIdAsync(Guid reportId)
        {
            ReportDetail? record = await dbContext.ReportDetail
                .Include(t => t.Report)
                .FirstOrDefaultAsync(p => p.ReportId == reportId);

            if (record == null)
                throw new NullReferenceException(nameof(record));

            return record;
        }

    }
}
