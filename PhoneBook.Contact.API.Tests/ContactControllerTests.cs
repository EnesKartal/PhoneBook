using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Contact.API.Controllers;
using PhoneBook.Contact.API.Models.DTO.Contact.AddContact;
using PhoneBook.Contact.API.Models.DTO.Contact.GetContact;
using PhoneBook.Contact.API.Services;

namespace PhoneBook.Contact.API.Tests
{
    public class ContactControllerTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithValidData()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var expectedData = new List<GetContactResponseDTO>
            {
                new GetContactResponseDTO { Id = Guid.NewGuid(),  Company = "Rise Consultant",FirstName ="Enes", LastName ="Kartal" },
                new GetContactResponseDTO { Id = Guid.NewGuid(), Company = "Rise Consultant-2",FirstName ="Enes-2", LastName ="Kartal-2" }
            };
            mockContactService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedData);
            var controller = new ContactController(mockContactService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var contactData = okResult.Value as List<GetContactResponseDTO>;
            Assert.Equal(expectedData, contactData);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithEmptyData()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var emptyData = new List<GetContactResponseDTO>();
            mockContactService.Setup(service => service.GetAllAsync()).ReturnsAsync(emptyData);
            var controller = new ContactController(mockContactService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var contactData = okResult.Value as List<GetContactResponseDTO>;
            Assert.Empty(contactData);
        }

        [Fact]
        public async Task Get_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var contactId = Guid.NewGuid();
            var contactData = new GetContactResponseDTO { Id = contactId, FirstName = "Enes", LastName = "Kartal", Company = "Rise Consultant" };
            mockContactService.Setup(service => service.GetAsync(contactId)).ReturnsAsync(contactData);
            var controller = new ContactController(mockContactService.Object);

            // Act
            var result = await controller.Get(contactId.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var contactResult = okResult.Value as GetContactResponseDTO;
            Assert.Equal(contactData, contactResult);
        }

        [Fact]
        public async Task Get_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var controller = new ContactController(mockContactService.Object);

            // Act
            var invalidId = "invalid-id";
            var result = await controller.Get(invalidId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Add_ShouldReturnOkResult_WithValidData()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var request = new AddContactRequestDTO
            {
                FirstName = "Enes",
                LastName = "Kartal",
                Company = "Rise",
            };
            var expectedResult = new AddContactResponseDTO
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Company = "Rise",
            };

            mockContactService.Setup(service => service.AddAsync(request)).ReturnsAsync(expectedResult);
            var controller = new ContactController(mockContactService.Object);

            // Act
            var result = await controller.Add(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var data = okResult.Value as AddContactResponseDTO;
            Assert.Equal(expectedResult, data);
        }

        [Fact]
        public async Task Remove_ShouldReturnOkResult_WithValidId()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var controller = new ContactController(mockContactService.Object);
            var validId = Guid.NewGuid().ToString();

            // Act
            var result = await controller.Remove(validId);

            // Assert
            Assert.IsType<OkResult>(result);
            mockContactService.Verify(service => service.RemoveAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnBadRequest_WithInvalidId()
        {
            // Arrange
            var mockContactService = new Mock<IContactService>();
            var controller = new ContactController(mockContactService.Object);
            var invalidId = "invalid-id";

            // Act
            var result = await controller.Remove(invalidId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}