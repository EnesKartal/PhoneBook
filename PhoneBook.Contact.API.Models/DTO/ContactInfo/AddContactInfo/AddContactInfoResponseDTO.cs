using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfo
{
    public class AddContactInfoResponseDTO : BaseDTO<Guid>
    {
        public Guid ContactId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
