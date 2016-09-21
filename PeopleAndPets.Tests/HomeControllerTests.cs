using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;
using System.Threading.Tasks;
using PeopleAndPets.Controllers;
using System.Web.Mvc;
using FluentAssertions;

namespace PeopleAndPets.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void Index_Returns_ViewResult()
        {
            // Arrange
            var mockSvcClient = new Mock<IPeopleApiClient>();
            mockSvcClient.Setup(client => client.GetPeopleAndTheirPets()).Returns(Task.FromResult(GetPeopleWithPets()));
            var controller = new HomeController(mockSvcClient.Object);

            // Act
            var result = await controller.Index();
            
            // Assert
            Assert.IsType<ViewResult>(result);
            
        }

        [Fact]
        public async void Index_Returns_ViewResult_WithData()
        {
            // Arrange
            var mockSvcClient = new Mock<IPeopleApiClient>();
            mockSvcClient.Setup(client => client.GetPeopleAndTheirPets()).Returns(Task.FromResult(GetPeopleWithPets()));
            var controller = new HomeController(mockSvcClient.Object);

            // Act
            var result = await controller.Index() as ViewResult;
            var model = (PetOwners) result.ViewData.Model;
            
            // Assert
            result.ViewData.Model.Should().BeOfType<PetOwners>();
            model.PetsOwnedByMale.Count.Should().Be(4);
            model.PetsOwnedByFemale.Count.Should().Be(3);
        }

        [Fact]
        public async void Index_Returns_ErrorView_IfNoCatsFound()
        {
            // Arrange
            var mockSvcClient = new Mock<IPeopleApiClient>();
            mockSvcClient.Setup(client => client.GetPeopleAndTheirPets()).Returns(Task.FromResult(GetEmptyResponse()));
            var controller = new HomeController(mockSvcClient.Object);

            // Act
            var result = await controller.Index();
            var content = result as ContentResult;

            // Assert
            result.Should().BeOfType<ContentResult>();

            content.Content.Should().Be("No Cats Found");
        }

        public static IEnumerable<People> GetPeopleWithPets()
        {
            return JsonConvert.DeserializeObject<IEnumerable<People>>("[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]");
        }

        public static IEnumerable<People> GetEmptyResponse()
        {
            return new List<People>();
        }
    }
}
