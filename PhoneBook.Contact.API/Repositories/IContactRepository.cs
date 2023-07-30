namespace PhoneBook.Contact.API.Repositories
{
    public interface IContactRepository
    {
        Task<Models.Domain.Entities.Contact> GetAsync(Guid id);
        Task<IEnumerable<Models.Domain.Entities.Contact>> GetAllAsync();
        Task<Models.Domain.Entities.Contact> AddAsync(Models.Domain.Entities.Contact contact);
        Task RemoveAsync(Guid id);
    }
}
