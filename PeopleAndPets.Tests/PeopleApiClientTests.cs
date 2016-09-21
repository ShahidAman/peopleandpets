using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PeopleAndPets.Tests
{
    public class PeopleApiClientTests
    {
        [Fact]
        public async void GetPeopleAndTheirPets_ShouldReturnValidObject()
        {
            var mockClient = new Mock<IPeopleApiClient>();
            mockClient.Setup(client => client.GetPeopleAndTheirPets()).Returns(Task.FromResult(HomeControllerTests.GetPeopleWithPets()));

            var response = await mockClient.Object.GetPeopleAndTheirPets();

            response.Should().BeOfType<List<People>>();
        }
    }
}
