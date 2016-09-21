using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace PeopleAndPets
{
    public class PeopleApiClient : IPeopleApiClient
    {
        private readonly HttpClient webSvcHttpClient;
        private readonly string webServiceBaseUrl = ConfigurationManager.AppSettings["PeopleWebService.Url"];

        private bool disposed = false;


        public PeopleApiClient()
        {
            webSvcHttpClient = new HttpClient();
        }


        public async Task<IEnumerable<People>> GetPeopleAndTheirPets()
        {
            Contract.Ensures(Contract.Result<IEnumerable<People>>() != null);

            HttpResponseMessage response = await webSvcHttpClient.GetAsync(webServiceBaseUrl);

            var responseData = await response.Content.ReadAsStringAsync();
            var peopleResponse = JsonConvert.DeserializeObject<IList<People>>(responseData);

            return peopleResponse;

        }

        ~PeopleApiClient()
        {
            if (webSvcHttpClient != null)
            {
                webSvcHttpClient.Dispose();
            }
        }
    }
}