using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Constants;
using PhoneBook.Common.Models.Extra.RabbitMQ;
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
            var deleteEntity = await dbContext.Contact.FindAsync(id);
            
            dbContext.Contact.Remove(deleteEntity);
            await dbContext.SaveChangesAsync();
        }
        public async Task<ReportResponseModel> GetReport(ReportRequestModel request)
        {
            Guid[] contactsInLocation = await dbContext.Contact.Where(
                p => p.ContactInfos.Any(
                    c => c.Type == ContactInfoTypeConstants.LOCATION && c.Content == request.Location))
                .Select(p => p.Id).ToArrayAsync();

            int phoneNumberCount = await dbContext.ContactInfo.Where(p => contactsInLocation.Contains(p.ContactId)).CountAsync();

            ReportResponseModel reportResponseModel = new ReportResponseModel();
            reportResponseModel.Id = request.Id;
            reportResponseModel.PeopleCount = contactsInLocation.Length;
            reportResponseModel.PhoneNumberCount = phoneNumberCount;
            reportResponseModel.Location = request.Location;

            return reportResponseModel;
        }
    }
}
