using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Constants;
using PhoneBook.Common.Models.Extra.RabbitMQ;
using PhoneBook.Contact.API.Models.Domain;
using PhoneBook.Contact.API.Models.Domain.Entities;
using PhoneBook.Contact.API.Repositories;

namespace PhoneBook.Contact.API.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public async Task GetAsync_ShouldReturnContact_IfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var contact = new Models.Domain.Entities.Contact
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Enes",
                    LastName = "Kartal",
                    Company = "Rise"
                };

                context.Contact.Add(contact);
                await context.SaveChangesAsync();

                // Act
                var result = await repository.GetAsync(contact.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(contact.Id, result.Id);
            }
        }

        [Fact]
        public async Task GetAsync_ShouldThrowNullReferenceException_IfNotExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                // Act & Assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await repository.GetAsync(Guid.NewGuid()));
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllContacts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var allList = context.Contact.ToList();
                context.Contact.RemoveRange(allList);
                await context.SaveChangesAsync();

                var contacts = new List<Models.Domain.Entities.Contact>
                {
                    new Models.Domain.Entities.Contact
                    {
                        Id = Guid.NewGuid(),
                        Company ="Rise",
                        FirstName="Enes",
                        LastName="Kartal"
                    },
                    new Models.Domain.Entities.Contact
                    {
                        Id = Guid.NewGuid(),
                        FirstName ="Enes-1",
                        LastName="Kartal-1",
                        Company="Rise-1"
                    },
                };

                context.Contact.AddRange(contacts);
                context.SaveChanges();

                // Act
                var result = await repository.GetAllAsync();
                // Assert
                Assert.NotNull(result);
                Assert.Equal(contacts.Count, result.Count());
                Assert.Equal(contacts.Select(c => c.Id).OrderBy(p => p).ToList(), result.Select(c => c.Id).OrderBy(p => p).ToList());
            }
        }

        [Fact]
        public async Task AddAsync_ShouldAddContactToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var contact = new Models.Domain.Entities.Contact
                {
                    Id = Guid.NewGuid(),
                    Company = "Rise",
                    FirstName = "Enes",
                    LastName = "Kartal"
                };

                // Act
                var result = await repository.AddAsync(contact);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(contact.Id, result.Id);

                // Check if the contact has been added to the database
                var addedContact = await context.Contact.FindAsync(contact.Id);
                Assert.NotNull(addedContact);
                // You can also check other properties of the addedContact here if needed
            }
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveContactFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var contact = new Models.Domain.Entities.Contact
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Enes",
                    LastName = "Kartal",
                    Company = "Rise",
                };

                await context.Contact.AddAsync(contact);
                await context.SaveChangesAsync();

                // Act
                await repository.RemoveAsync(contact.Id);

                // Assert
                var removedContact = await context.Contact.FindAsync(contact.Id);
                Assert.Null(removedContact);
            }
        }

        [Fact]
        public async Task GetReport_ShouldReturnCorrectReportResponseModel_WhenLocationExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var location = "Test_Location";

                var contacts = new List<Models.Domain.Entities.Contact>
                {
                    new Models.Domain.Entities.Contact
                    {
                        Id = Guid.NewGuid(),
                        Company ="Rise",
                        FirstName="Enes",
                        LastName="Kartal"
                        // Set other properties of the contact here
                    },
                    new Models.Domain.Entities.Contact
                    {
                        Id = Guid.NewGuid(),
                        Company ="Rise-1",
                        FirstName="Kartal-1",
                        LastName="Kartal-1"
                        // Set other properties of the contact here
                    },
                    // Add more contacts here as needed
                };

                context.Contact.AddRange(contacts);

                var contactInfos = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        ContactId = contacts[0].Id,
                        Type = ContactInfoTypeConstants.LOCATION,
                        Content = location,
                    },
                    new ContactInfo
                    {
                        Id = Guid.NewGuid(),
                        ContactId = contacts[1].Id,
                        Type = ContactInfoTypeConstants.LOCATION,
                        Content = location,
                    },
                    // Add more contactInfos here as needed
                };

                context.ContactInfo.AddRange(contactInfos);
                await context.SaveChangesAsync();

                var request = new ReportRequestModel
                {
                    Id = Guid.NewGuid(),
                    Location = location,
                };

                // Act
                var result = await repository.GetReport(request);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(request.Id, result.Id);
                Assert.Equal(contacts.Count, result.PeopleCount);
                Assert.Equal(contactInfos.Count, result.PhoneNumberCount);
                Assert.Equal(request.Location, result.Location);
            }
        }

        [Fact]
        public async Task GetReport_ShouldReturnEmptyReportResponseModel_WhenLocationDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookContactDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new PhoneBookContactDbContext(options))
            {
                var repository = new ContactRepository(context);

                var location = "Non_Existing_Location";

                var request = new ReportRequestModel
                {
                    Id = Guid.NewGuid(),
                    Location = location,
                };

                // Act
                var result = await repository.GetReport(request);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(request.Id, result.Id);
                Assert.Equal(0, result.PeopleCount);
                Assert.Equal(0, result.PhoneNumberCount);
                Assert.Equal(request.Location, result.Location);
            }
        }


    }
}
