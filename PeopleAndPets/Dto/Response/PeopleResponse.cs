using System.Collections.Generic;

namespace PeopleAndPets
{
    public class People
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public IEnumerable<Pet> Pets { get; set; }
    }

    public class Pet
    {
        public string Name { get; set; }

        public string Type { get; set; }

    }
}