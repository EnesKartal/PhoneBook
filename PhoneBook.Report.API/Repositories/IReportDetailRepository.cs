using PhoneBook.Report.API.Models.Domain.Entities;

namespace PhoneBook.Report.API.Repositories
{
    public interface IReportDetailRepository
    {
        Task<ReportDetail> GetByReportIdAsync(Guid reportId);
        Task<ReportDetail> AddAsync(ReportDetail reportDetail);
    }
}
