using Microsoft.EntityFrameworkCore;
using PhoneBook.Report.API.Models.Domain.Entities;

namespace PhoneBook.Report.API.Models.Domain
{
    public class PhoneBookReportDbContext : DbContext
    {
        public PhoneBookReportDbContext(DbContextOptions<PhoneBookReportDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Report> Report { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }
    }
}
