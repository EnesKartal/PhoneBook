using PhoneBook.Common.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Report.API.Models.Domain.Entities
{
    public class ReportDetail : BaseEntity
    {
        public Guid ReportId { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }

        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
    }
}
