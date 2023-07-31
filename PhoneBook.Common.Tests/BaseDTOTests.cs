using PhoneBook.Common.Models.DTO;

namespace PhoneBook.Common.Tests
{
    public class BaseDTOTests
    {
        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            int expectedId = 42;
            BaseDTO<int> dto = new BaseDTO<int>();

            // Act
            dto.Id = expectedId;
            int actualId = dto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void SetIdGetsCorrectValue()
        {
            // Arrange
            int expectedId = 100;
            BaseDTO<int> dto = new BaseDTO<int>();

            // Act
            dto.Id = expectedId;
            int actualId = dto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }
    }
}