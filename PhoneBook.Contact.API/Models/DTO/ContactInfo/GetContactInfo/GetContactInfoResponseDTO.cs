using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Contact.API.Models.DTO.ContactInfo.GetContactInfo
{
    public class GetContactInfoResponseDTO : BaseDTO<Guid>
    {
        public Guid ContactId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
