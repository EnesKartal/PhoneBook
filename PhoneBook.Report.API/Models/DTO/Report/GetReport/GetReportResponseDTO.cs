using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Report.API.Models.DTO.Report.GetReport
{
    public class GetReportResponseDTO : BaseDTO<Guid>
    {
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
