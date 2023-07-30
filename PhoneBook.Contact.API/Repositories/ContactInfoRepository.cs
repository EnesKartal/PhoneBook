using PhoneBook.Contact.API.Models.Domain.Entities;
using PhoneBook.Contact.API.Models.Domain;

namespace PhoneBook.Contact.API.Repositories
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly PhoneBookContactDbContext dbContext;

        public ContactInfoRepository(PhoneBookContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ContactInfo> AddAsync(ContactInfo contactInfo)
        {
            await dbContext.ContactInfo.AddAsync(contactInfo);
            await dbContext.SaveChangesAsync();
            return contactInfo;
        }

        public async Task RemoveAsync(Guid id)
        {
            ContactInfo record = new ContactInfo
            {
                Id = id,
            };

            dbContext.ContactInfo.Attach(record);
            dbContext.ContactInfo.Remove(record);
            await dbContext.SaveChangesAsync();
        }
    }
}
