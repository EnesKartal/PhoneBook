using PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfoRequestDTO;

namespace PhoneBook.Contact.API.Services
{
    public interface IContactInfoService
    {
        Task<AddContactInfoResponseDTO> AddAsync(AddContactInfoRequestDTO contactInfo);
        Task RemoveAsync(Guid id);
    }
}
