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
            if (string.IsNullOrEmpty(contactInfo.Content) || string.IsNullOrEmpty(contactInfo.Type))
            {
                throw new ArgumentException("Content and Type properties are required for ContactInfo.");
            }

            await dbContext.ContactInfo.AddAsync(contactInfo);
            await dbContext.SaveChangesAsync();
            return contactInfo;
        }

        public async Task RemoveAsync(Guid id)
        {
            ContactInfo record = await dbContext.ContactInfo.FindAsync(id);
            if (record == null)
                throw new ArgumentNullException();

            dbContext.ContactInfo.Remove(record);
            await dbContext.SaveChangesAsync();
        }
    }
}
