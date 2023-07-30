using PhoneBook.Report.API.Models.DTO.Report.GetReport;

namespace PhoneBook.Report.API.Services
{
    public interface IReportService
    {
        Task<GetReportResponseDTO> GetAsync(Guid id);
        Task<IEnumerable<GetReportResponseDTO>> GetAllAsync();
        Task RequestReportAsync(string location); //TODO: Burası parametrik mi olacak onu bir öğren.
        Task UpdateReportStatusAsync(Guid id);
    }
}
