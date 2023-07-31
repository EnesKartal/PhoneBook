using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Common.Constants;
using PhoneBook.Contact.API.Controllers;
using PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfoRequestDTO;
using PhoneBook.Contact.API.Services;

namespace PhoneBook.Contact.API.Tests
{
    public class ContactInfoControllerTests
    {
        [Fact]
        public async Task Add_ShouldReturnOkResult_WithValidData()
        {
            // Arrange
            var mockContactInfoService = new Mock<IContactInfoService>();
            var expectedData = new AddContactInfoResponseDTO { Id = Guid.NewGuid(), ContactId = Guid.NewGuid(), Type = ContactInfoTypeConstants.EMAIL, Content = "eneskartal117@gmail.com" };
            mockContactInfoService.Setup(service => service.AddAsync(It.IsAny<AddContactInfoRequestDTO>())).ReturnsAsync(expectedData);
            var controller = new ContactInfoController(mockContactInfoService.Object);
            var request = new AddContactInfoRequestDTO { ContactId = Guid.NewGuid(), Type = ContactInfoTypeConstants.EMAIL, Content = "eneskartal117@gmail.com" };

            // Act
            var result = await controller.Add(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            var contactInfoData = okResult.Value as AddContactInfoResponseDTO;
            Assert.Equal(expectedData.Id, contactInfoData.Id);
            Assert.Equal(expectedData.ContactId, contactInfoData.ContactId);
            Assert.Equal(expectedData.Type, contactInfoData.Type);
            Assert.Equal(expectedData.Content, contactInfoData.Content);
        }

        [Fact]
        public async Task Add_ShouldReturnBadRequest_WithInvalidData()
        {
            // Arrange
            var mockContactInfoService = new Mock<IContactInfoService>();
            var controller = new ContactInfoController(mockContactInfoService.Object);
            var request = new AddContactInfoRequestDTO { Type = ContactInfoTypeConstants.EMAIL, Content = "eneskartal117@gmail.com" };

            // Act
            var result = await controller.Add(request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Remove_ShouldReturnOkResult_WithValidId()
        {
            // Arrange
            var mockContactInfoService = new Mock<IContactInfoService>();
            var controller = new ContactInfoController(mockContactInfoService.Object);
            var contactInfoId = Guid.NewGuid();

            // Act 
            var result = await controller.Remove(contactInfoId.ToString());

            // Assert
            Assert.IsType<OkResult>(result);
            mockContactInfoService.Verify(service => service.RemoveAsync(contactInfoId), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnBadRequest_WithInvalidId()
        {
            // Arrange
            var mockContactInfoService = new Mock<IContactInfoService>();
            var controller = new ContactInfoController(mockContactInfoService.Object);
            var invalidId = "invalid-id";

            // Act
            var result = await controller.Remove(invalidId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            mockContactInfoService.Verify(service => service.RemoveAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
