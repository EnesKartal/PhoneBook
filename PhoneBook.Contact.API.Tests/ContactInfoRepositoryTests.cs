using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Constants;
using PhoneBook.Contact.API.Models.Domain;
using PhoneBook.Contact.API.Models.Domain.Entities;
using PhoneBook.Contact.API.Repositories;

namespace PhoneBook.Contact.API.Tests
{
    public class ContactInfoRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddContactInfoToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactInfoRepository(context);

                var contactInfo = new ContactInfo
                {
                    // Set properties of contactInfo here
                    ContactId = Guid.NewGuid(),
                    Content = "Istanbul",
                    Type = ContactInfoTypeConstants.LOCATION
                };

                // Act
                var addedContactInfo = await repository.AddAsync(contactInfo);

                // Assert
                Assert.NotNull(addedContactInfo);
                Assert.Equal(contactInfo.Id, addedContactInfo.Id);

                // Verify that the contactInfo is actually added to the database
                var retrievedContactInfo = await context.ContactInfo.FindAsync(contactInfo.Id);
                Assert.NotNull(retrievedContactInfo);
                Assert.Equal(contactInfo.Id, retrievedContactInfo.Id);
            }
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveContactInfoFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactInfoRepository(context);

                var contactInfo = new ContactInfo
                {
                    Id = Guid.NewGuid(),
                    Type = ContactInfoTypeConstants.LOCATION,
                    Content = "Ankara"
                };

                // Add the contactInfo to the database for testing removal
                await context.ContactInfo.AddAsync(contactInfo);
                await context.SaveChangesAsync();

                // Act
                await repository.RemoveAsync(contactInfo.Id);

                // Assert
                // Verify that the contactInfo is actually removed from the database
                var removedContactInfo = await context.ContactInfo.FindAsync(contactInfo.Id);
                Assert.Null(removedContactInfo);
            }
        }
    }
}
