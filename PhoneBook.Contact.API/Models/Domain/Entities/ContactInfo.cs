using PhoneBook.Common.Models.Entities;

namespace PhoneBook.Contact.API.Models.Domain.Entities
{
    public class ContactInfo : BaseEntity
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
