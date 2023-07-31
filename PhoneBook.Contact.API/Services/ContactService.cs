using PhoneBook.Common.Interfaces;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Contact.API.Models.DTO.Contact.AddContact;
using PhoneBook.Contact.API.Models.DTO.Contact.GetContact;
using PhoneBook.Contact.API.Models.DTO.ContactInfo.GetContactInfo;
using PhoneBook.Contact.API.Repositories;

namespace PhoneBook.Contact.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IRabbitMQProducer rabbitMQProducer;

        public ContactService(IContactRepository contactRepository, IRabbitMQProducer rabbitMQProducer)
        {
            this.contactRepository = contactRepository;
            this.rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<GetContactResponseDTO> GetAsync(Guid id)
        {
            Models.Domain.Entities.Contact record = await contactRepository.GetAsync(id);

            if (record == null)
                throw new Exception("Record not found");

            GetContactResponseDTO response = new GetContactResponseDTO()
            {
                Company = record.Company,
                FirstName = record.FirstName,
                LastName = record.LastName,
                Id = record.Id,
                ContactInfos = record.ContactInfos.Select(p => new GetContactInfoResponseDTO
                {
                    Id = p.Id,
                    ContactId = p.ContactId,
                    Content = p.Content,
                    Type = p.Type
                }).ToList()
            };

            return response;
        }

        public async Task<IEnumerable<GetContactResponseDTO>> GetAllAsync()
        {
            IEnumerable<Models.Domain.Entities.Contact> contacts = await contactRepository.GetAllAsync();

            List<GetContactResponseDTO> records = contacts.Select(p => new GetContactResponseDTO()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Company = p.Company,
            }).ToList();

            return records;
        }

        public async Task<AddContactResponseDTO> AddAsync(AddContactRequestDTO contact)
        {
            Models.Domain.Entities.Contact record = new Models.Domain.Entities.Contact
            {
                Id = Guid.NewGuid(),
                Company = contact.Company,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };

            Models.Domain.Entities.Contact entity = await contactRepository.AddAsync(record);

            AddContactResponseDTO response = new AddContactResponseDTO
            {
                Id = entity.Id,
                Company = entity.Company,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };

            return response;
        }

        public async Task RemoveAsync(Guid id)
        {
            await contactRepository.RemoveAsync(id);
        }

        public Task<ReportResponseModel> GetReport(ReportRequestModel request)
        {
            return contactRepository.GetReport(request);
        }

        public async Task PrepareReport(ReportRequestModel request)
        {
            //10 seconds delay for better experience
            Thread.Sleep(10000);
            ReportResponseModel response = await GetReport(request);
            rabbitMQProducer.SendMessage(response);
        }
    }
}