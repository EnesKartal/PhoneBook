using PhoneBook.Contact.API.Models.Domain.Entities;

namespace PhoneBook.Contact.API.Repositories
{
    public interface IContactInfoRepository
    {
        Task<ContactInfo> AddAsync(ContactInfo contactInfo);
        Task RemoveAsync(Guid id);
    }
}
