using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Contact.API.Models.DTO.Contact.AddContact;
using PhoneBook.Contact.API.Models.DTO.Contact.GetContact;

namespace PhoneBook.Contact.API.Services
{
    public interface IContactService
    {
        Task<GetContactResponseDTO> Get(Guid id);
        Task<IEnumerable<GetContactResponseDTO>> GetAll();
        Task<AddContactResponseDTO> Add(AddContactRequestDTO contact);
        Task Remove(Guid id);
        Task PrepareReport(ReportRequestModel request);
    }
}
