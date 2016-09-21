using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PeopleAndPets
{
    public interface IPeopleApiClient
    {
        Task<IEnumerable<People>> GetPeopleAndTheirPets();
    }
}