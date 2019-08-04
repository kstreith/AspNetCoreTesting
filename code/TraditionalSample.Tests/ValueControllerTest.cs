using FluentAssertions;
using TraditionalSample.Controllers;
using Xunit;

namespace TraditionalSample.Tests
{
    public class ValueControllerTest
    {
        private ValuesController Arrange()
        {
            return new ValuesController();
        }

        [Fact]
        public void GetValues_Works()
        {
            // Arrange
            var controller = Arrange();

            // Act
            var result = controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Contain("value1", "value2");
        }

        [Fact]
        public void GetValue_Works()
        {
            // Arrange
            var controller = Arrange();

            // Act
            var result = controller.Get(5);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Contain("value");
        }
    }
}
