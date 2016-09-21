using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PeopleAndPets.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleApiClient peopleApiClient;// = new PeopleApiClient();

        public HomeController(IPeopleApiClient peopleSvcClient)
        {
            this.peopleApiClient = peopleSvcClient;
        }

        [HandleError]
        public async Task<ActionResult> Index()
        {
            var peopleAndPets = await peopleApiClient.GetPeopleAndTheirPets();

            var model = new PetOwners
            {
                PetsOwnedByMale = peopleAndPets.GetAllCatsOwnedByMales(),
                PetsOwnedByFemale = peopleAndPets.GetAllCatsOwnedByFemales()
            };

            // if no cats
            if(model.PetsOwnedByFemale.Count == 0 && model.PetsOwnedByMale.Count == 0)
            {
                return Content("No Cats Found");
            }
            
            return View(model);
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "People and Pets.";

            return View();
        }

    }
}