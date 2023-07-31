using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Contact.API.Models.DTO.Contact.AddContact;
using PhoneBook.Contact.API.Models.DTO.Contact.GetContact;

namespace PhoneBook.Contact.API.Services
{
    public interface IContactService
    {
        Task<GetContactResponseDTO> GetAsync(Guid id);
        Task<IEnumerable<GetContactResponseDTO>> GetAllAsync();
        Task<AddContactResponseDTO> AddAsync(AddContactRequestDTO contact);
        Task RemoveAsync(Guid id);
    }
}
