using Microsoft.EntityFrameworkCore;
using PhoneBook.Report.API.Models.Domain;

namespace PhoneBook.Report.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly PhoneBookReportDbContext dbContext;
        public ReportRepository(PhoneBookReportDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Models.Domain.Entities.Report report)
        {
            await dbContext.Report.AddAsync(report);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateReportStatusAsync(Guid id)
        {
            Models.Domain.Entities.Report? entity = await dbContext.Report.FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
                throw new ArgumentNullException();

            //TODO: change it to const
            entity.Status = "Completed";

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Domain.Entities.Report>> GetAllAsync()
        {
            return await dbContext.Report.ToListAsync();
        }
        public async Task<Models.Domain.Entities.Report> GetAsync(Guid id)
        {
            return await dbContext.Report.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
