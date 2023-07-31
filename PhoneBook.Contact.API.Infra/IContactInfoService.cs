using PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfo;

namespace PhoneBook.Contact.API.Infra
{
    public interface IContactInfoService
    {
        Task<AddContactInfoResponseDTO> AddAsync(AddContactInfoRequestDTO contactInfo);
        Task RemoveAsync(Guid id);
    }
}