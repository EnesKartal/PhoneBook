using Microsoft.EntityFrameworkCore;
using PhoneBook.Contact.API.Models.Domain;

namespace PhoneBook.Contact.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhoneBookContactDbContext dbContext;

        public ContactRepository(PhoneBookContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Models.Domain.Entities.Contact> GetAsync(Guid id)
        {
            Models.Domain.Entities.Contact? record = await dbContext.Contact
           .Include(t => t.ContactInfos)
           .FirstOrDefaultAsync(p => p.Id == id);

            if (record == null)
                throw new NullReferenceException(nameof(record));

            return record;
        }
        public async Task<IEnumerable<Models.Domain.Entities.Contact>> GetAllAsync()
        {
            return await dbContext.Contact.ToListAsync();
        }
        public async Task<Models.Domain.Entities.Contact> AddAsync(Models.Domain.Entities.Contact contact)
        {
            await dbContext.Contact.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return contact;
        }
        public async Task RemoveAsync(Guid id)
        {
            Models.Domain.Entities.Contact record = new Models.Domain.Entities.Contact
            {
                Id = id,
            };

            dbContext.Contact.Attach(record);
            dbContext.Contact.Remove(record);
            await dbContext.SaveChangesAsync();
        }
    }
}
