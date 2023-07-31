using PhoneBook.Common.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Contact.API.Models.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        [ForeignKey("ContactId")]
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
