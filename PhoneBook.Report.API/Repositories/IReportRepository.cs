namespace PhoneBook.Report.API.Repositories
{
    public interface IReportRepository
    {
        Task<Models.Domain.Entities.Report> GetAsync(Guid id);
        Task AddAsync(Models.Domain.Entities.Report report);
        Task UpdateReportStatusAsync(Guid id);
        Task<IEnumerable<Models.Domain.Entities.Report>> GetAllAsync();
    }
}
