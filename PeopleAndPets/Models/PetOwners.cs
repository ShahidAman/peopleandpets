using System.Collections.Generic;

namespace PeopleAndPets
{
    public class PetOwners
    {
        public IList<Pet> PetsOwnedByMale { get; set; }
        public IList<Pet> PetsOwnedByFemale { get; set; }
    }

}