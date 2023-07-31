using PhoneBook.Common.Constants;
using PhoneBook.Common.Interfaces;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Report.API.Models.DTO.Report.GetReport;
using PhoneBook.Report.API.Repositories;

namespace PhoneBook.Report.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;
        private readonly IReportDetailService reportDetailService;
        private readonly IRabbitMQProducer rabbitMQProducer;

        public ReportService(IReportRepository reportRepository, IReportDetailService reportDetailService, IRabbitMQProducer rabbitMQProducer)
        {
            this.reportRepository = reportRepository;
            this.reportDetailService = reportDetailService;
            this.rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<IEnumerable<GetReportResponseDTO>> GetAllAsync()
        {
            var reports = await reportRepository.GetAllAsync();

            List<GetReportResponseDTO> records = reports.Select(p => new GetReportResponseDTO
            {
                Id = p.Id,
                Location = p.Location,
                RequestDate = p.RequestDate,
                Status = p.Status
            }).ToList();

            return records;
        }

        public async Task<GetReportResponseDTO> GetAsync(Guid id)
        {
            var report = await reportRepository.GetAsync(id);

            GetReportResponseDTO response = new GetReportResponseDTO
            {
                Id = report.Id,
                Location = report.Location,
                RequestDate = report.RequestDate,
                Status = report.Status
            };

            return response;
        }

        public async Task RequestReportAsync(string location)
        {
            Models.Domain.Entities.Report record = new Models.Domain.Entities.Report
            {
                Id = Guid.NewGuid(),
                RequestDate = DateTime.Now,
                Status = ReportConstants.PREPARING,
                Location = location
            };

            await reportRepository.AddAsync(record);

            rabbitMQProducer.SendMessage(new ReportRequestModel
            {
                Id = record.Id,
                Location = record.Location
            });
        }

        public async Task UpdateReportStatusAsync(Guid id)
        {
            await reportRepository.UpdateReportStatusAsync(id);
        }

        public async Task ReportCompleteActionAsync(ReportResponseModel response)
        {
            await reportDetailService.AddAsync(response);
            await UpdateReportStatusAsync(response.Id);
        }
    }
}
