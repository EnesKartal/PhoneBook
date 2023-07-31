using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Contact.API.Models.DTO.Contact.AddContact
{
    public class AddContactResponseDTO : BaseDTO<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
