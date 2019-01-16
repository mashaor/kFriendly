using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
using kFriendly.Infrastructure;
using kFriendly.UI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace kFriendly.UI.Controllers
{
    public class BusinessController : Controller
    {
        readonly IQueryBusiness queryBusiness;
        public BusinessController(IQueryBusiness queryBusiness)
        {
            this.queryBusiness = queryBusiness;
        }


        public ActionResult Search()
        {
            return View(new SearchBusinessModel());
        }

        [HttpPost]
        public async Task<ActionResult> Search(string term, string location)
        {
            SearchRequest searchCriteria = new SearchRequest();
            searchCriteria.Term = term;
            searchCriteria.Location = location;

            BusinessSearchResponse allBusinesses = await queryBusiness.GetBusinessByCriteria(searchCriteria);

            return View("BusinessSearchResults", allBusinesses);
        }


        private async Task<BusinessSearchResponse> SearchBla()
        {
            using (HttpClient Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.API_KEY_YELP);
                var response = await Client.GetAsync(@"https://api.yelp.com/v3/autocomplete?latitude=33.7325566&longitude=-118.0010307&text=piz", new CancellationToken());
                var response2 =  Client.GetAsync(@"https://api.yelp.com/v3/autocomplete?latitude=33.7325566&longitude=-118.0010307&text=piz", new CancellationToken());
                var data = await response.Content.ReadAsStringAsync();

                var jsonModel = JsonConvert.DeserializeObject<BusinessSearchResponse>(data);

                return jsonModel;
                
            }
        }

        [HttpPost]
        public async Task<ActionResult> Autocomplete(string term, string latitude, string longitude)
        {
            SearchRequest searchCriteria = new SearchRequest();
            searchCriteria.Latitude = double.Parse(latitude);
            searchCriteria.Longitude = double.Parse(longitude);
            searchCriteria.Text = term;
            // searchCriteria.Locale = locale;

            List<string> suggestions = await queryBusiness.Autocomplete(searchCriteria);
            
            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusinessDetails(YelpBusiness business)
        {
            // var allBusinesses = queryBusiness.GetBusinessByCriteria(criteria);

            return View(business);
        }
    }
}