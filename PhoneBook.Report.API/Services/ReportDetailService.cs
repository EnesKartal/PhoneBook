using PhoneBook.Report.API.Models.DTO.ReportDetail.GetReportDetail;
using PhoneBook.Report.API.Repositories;

namespace PhoneBook.Report.API.Services
{
    public class ReportDetailService : IReportDetailService
    {
        private readonly IReportDetailRepository reportDetailRepository;
        public ReportDetailService(IReportDetailRepository reportDetailRepository)
        {
            this.reportDetailRepository = reportDetailRepository;
        }

        public async Task<GetReportDetailDTO> GetByReportIdAsync(Guid reportId)
        {
            var entity = await reportDetailRepository.GetByReportIdAsync(reportId);

            GetReportDetailDTO response = new GetReportDetailDTO
            {
                Id = entity.Id,
                Location = entity.Report.Location,
                PeopleCount = entity.PeopleCount,
                PhoneNumberCount = entity.PhoneNumberCount,
                ReportId = entity.ReportId,
                ReportRequesDate = entity.Report.RequestDate
            };

            return response;
        }
    }
}
