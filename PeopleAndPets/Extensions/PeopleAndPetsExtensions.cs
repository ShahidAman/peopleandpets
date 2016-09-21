using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleAndPets
{
    public static class PeopleAndPetsExtensions
    {
        public static IList<Pet> GetAllPetsByGender(this IEnumerable<People> peopleAndPets, string gender, string petType)
        {
            var petsByOwnerGender = new List<Pet>();

            peopleAndPets.Where(p => p.Gender.Equals(gender, StringComparison.InvariantCultureIgnoreCase) && p.Pets != null)
                .ToList()
                .ForEach(item =>
                {
                    petsByOwnerGender = item.Pets.Where(p => p.Type == petType).ToList();
                });

            return petsByOwnerGender;
        }

        public static IList<Pet> GetAllCatsOwnedByMales(this IEnumerable<People> peopleAndPets)
        {
            var catsOwnedBymale = new List<Pet>();
            peopleAndPets.Where(p => p.Gender.Equals("Male", StringComparison.InvariantCultureIgnoreCase) && p.Pets != null)
                .ToList()
                .ForEach(item =>
                {
                    item.Pets.Where(p => p.Type == "Cat").ToList().ForEach(cat =>
                    {
                        catsOwnedBymale.Add(cat);
                    });

                });

            return catsOwnedBymale.OrderBy(x => x.Name).ToList();
        }

        public static IList<Pet> GetAllCatsOwnedByFemales(this IEnumerable<People> peopleAndPets)
        {
            var catsOwnedByFemale = new List<Pet>();

            peopleAndPets.Where(p => p.Gender.Equals("Female", StringComparison.InvariantCultureIgnoreCase) && p.Pets != null)
                .ToList()
                .ForEach(item =>
                {
                    item.Pets.Where(p => p.Type == "Cat").ToList().ForEach(cat =>
                    {
                        catsOwnedByFemale.Add(cat);
                    });

                });

            return catsOwnedByFemale.OrderBy(x => x.Name).ToList();
        }
    }
}