using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MiddlewareSample.Exceptions;
using TraditionalSample.Controllers;
using TraditionalSample.Models;
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
        public void GetValue_Exists_Works()
        {
            // Arrange
            var controller = Arrange();

            // Act
            var result = controller.Get(1);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Contain("value");
        }

        [Fact]
        public void GetValue_NotFound_Throws()
        {
            // Arrange
            var controller = Arrange();

            // Act
            void Act() => controller.Get(5);

            // Assert
            Assert.Throws<NotFoundException>(new Action(Act));
        }

        [Fact]
        public void Post_ValidValue_Works()
        {
            // Arrange
            var controller = Arrange();

            // Act
            var requestObj = new ValueModel
            {
                Name = "TestName",
                Age = 22
            };
            var result = controller.Post(requestObj);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdResult.ControllerName.Should().BeNull();
            createdResult.ActionName.Should().Be(nameof(ValuesController.Get));
            createdResult.Value.Should().BeNull();
            createdResult.RouteValues.Should().ContainKey("id");
        }

        [Fact]
        public void Post_InvalidValue_Works()
        {
            var controller = Arrange();

            // Act
            var requestObj = new ValueModel
            {
                Age = -2
            };
            var _ = controller.Post(requestObj);

            // Assert
            //How to test that result is ProblemDetails result?
            //Maybe use TestHost and WebApplicationFactory instead?
        }
    }
}
