namespace PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfoRequestDTO
{
    public class AddContactInfoRequestDTO
    {
        public Guid ContactId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
