using PhoneBook.Contact.API.Models.Domain.Entities;
using PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfoRequestDTO;
using PhoneBook.Contact.API.Repositories;

namespace PhoneBook.Contact.API.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IContactInfoRepository contactInfoRepository;
        public ContactInfoService(IContactInfoRepository contactInfoRepository)
        {
            this.contactInfoRepository = contactInfoRepository;
        }

        public async Task<AddContactInfoResponseDTO> AddAsync(AddContactInfoRequestDTO contactInfo)
        {
            ContactInfo record = new ContactInfo
            {
                Id = Guid.NewGuid(),
                ContactId = contactInfo.ContactId,
                Content = contactInfo.Content,
                Type = contactInfo.Type
            };

            var entity = await contactInfoRepository.AddAsync(record);

            AddContactInfoResponseDTO response = new AddContactInfoResponseDTO
            {
                Id = entity.Id,
                Content = entity.Content,
                ContactId = entity.ContactId,
                Type = entity.Type
            };

            return response;
        }

        public async Task RemoveAsync(Guid id)
        {
            await contactInfoRepository.RemoveAsync(id);
        }
    }
}
