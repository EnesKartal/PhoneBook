using PhoneBook.Common.Models.DTO;
using PhoneBook.Contact.API.Models.DTO.ContactInfo.GetContactInfo;

namespace PhoneBook.Contact.API.Models.DTO.Contact.GetContact
{
    public class GetContactResponseDTO : BaseDTO<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        public List<GetContactInfoResponseDTO> ContactInfos { get; set; }
    }
}
