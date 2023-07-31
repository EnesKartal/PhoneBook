using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Report.API.Models.Domain.Entities;
using PhoneBook.Report.API.Models.DTO.ReportDetail.GetReportDetail;

namespace PhoneBook.Report.API.Services
{
    public interface IReportDetailService
    {
        Task<ReportDetail> AddAsync(ReportResponseModel response);
        Task<GetReportDetailDTO> GetByReportIdAsync(Guid id);
    }
}
