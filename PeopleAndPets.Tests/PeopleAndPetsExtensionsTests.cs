using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PeopleAndPets.Tests
{
    public class PeopleAndPetsExtensionsTests
    {
        [Fact]
        public void GetAllCatsOwnedByMales_ReturnOnlyCats()
        {
            var petsList = HomeControllerTests.GetPeopleWithPets();

            var catsOwnedByMale = petsList.GetAllCatsOwnedByMales();


            catsOwnedByMale.ToList().ForEach(pet =>
            {
                pet.Type.Should().Be("Cat");
            });
        }


        [Fact]
        public void GetAllCatsOwnedByFemales_ReturnOnlyCats()
        {
            var petsList = HomeControllerTests.GetPeopleWithPets();

            var catsOwnedByFemale = petsList.GetAllCatsOwnedByFemales();

            catsOwnedByFemale.ToList().ForEach(pet =>
            {
                pet.Type.Should().Be("Cat");
            });
        }
    }
}
