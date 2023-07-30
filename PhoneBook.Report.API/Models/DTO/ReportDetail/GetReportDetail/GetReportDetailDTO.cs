using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Report.API.Models.DTO.ReportDetail.GetReportDetail
{
    public class GetReportDetailDTO : BaseDTO<Guid>
    {
        public Guid ReportId { get; set; }
        public DateTime ReportRequesDate { get; set; }
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
