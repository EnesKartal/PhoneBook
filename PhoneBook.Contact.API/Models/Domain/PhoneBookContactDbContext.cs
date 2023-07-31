using Microsoft.EntityFrameworkCore;
using PhoneBook.Contact.API.Models.Domain.Entities;

namespace PhoneBook.Contact.API.Models.Domain
{
    public class PhoneBookContactDbContext : DbContext
    {
        public PhoneBookContactDbContext(DbContextOptions<PhoneBookContactDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Contact> Contact { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
    }
}
