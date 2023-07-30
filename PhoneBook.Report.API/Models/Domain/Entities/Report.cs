using PhoneBook.Common.Models.Entities;

namespace PhoneBook.Report.API.Models.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }

        public virtual ReportDetail ReportDetail { get; }
    }
}
