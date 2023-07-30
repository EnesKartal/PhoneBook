using PhoneBook.Report.API.Models.Domain.Entities;
using PhoneBook.Report.API.Models.DTO.ReportDetail.GetReportDetail;

namespace PhoneBook.Report.API.Services
{
    public interface IReportDetailService
    {
        Task<GetReportDetailDTO> GetByReportIdAsync(Guid id);
    }
}
