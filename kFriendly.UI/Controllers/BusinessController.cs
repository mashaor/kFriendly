using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
using kFriendly.Infrastructure;
using kFriendly.UI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        readonly IQueryGeocode queryGeocode;

        public BusinessController(IQueryBusiness queryBusiness, IQueryGeocode queryGeocode)
        {
            this.queryBusiness = queryBusiness;
            this.queryGeocode = queryGeocode;
        }

       // [HttpGet("Search")]
        public ActionResult Search()
        {
            return View(new SearchBusinessModel());
        }

        [HttpPost]
        public async Task<ActionResult> Search(string term, string location)
        {
            //ModelState.IsValid;
            //ModelState.Clear();
            KFBusinessModel allBusinesses = await queryBusiness.GetBusinessByCriteria(term, location);

           
            return View("BusinessSearchResults", allBusinesses);
        }


        //private async Task<BusinessSearchResponse> SearchBla()
        //{
        //    using (HttpClient Client = new HttpClient())
        //    {

        //        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.API_KEY_YELP);
        //        var response = await Client.GetAsync(@"https://api.yelp.com/v3/autocomplete?latitude=33.7325566&longitude=-118.0010307&text=piz", new CancellationToken());
        //        var response2 = Client.GetAsync(@"https://api.yelp.com/v3/autocomplete?latitude=33.7325566&longitude=-118.0010307&text=piz", new CancellationToken());
        //        var data = await response.Content.ReadAsStringAsync();

        //        var jsonModel = JsonConvert.DeserializeObject<BusinessSearchResponse>(data);

        //        return jsonModel;

        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> Autocomplete(string term, string latitude, string longitude)
        {
            List<string> suggestions = new List<string>();

            SearchRequest searchCriteria = new SearchRequest();

            double parsedLatitude;
            if (double.TryParse(latitude, out parsedLatitude))
            {
                searchCriteria.Latitude = parsedLatitude;
            }

            double parsedLongitude;
            if (double.TryParse(longitude, out parsedLongitude))
            {

                searchCriteria.Longitude = parsedLongitude;
            }

            // searchCriteria.Locale = locale;

            if (string.IsNullOrEmpty(term) == false)
            {
                searchCriteria.Text = term;

                suggestions = await queryBusiness.Autocomplete(searchCriteria);
            }

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetLocationFriendlyName(string latitude, string longitude)
        {
            

            string location = string.Empty;

            double parsedLatitude;
            double parsedLongitude;

            if (double.TryParse(latitude, out parsedLatitude) && double.TryParse(longitude, out parsedLongitude))
            {
                location = await queryGeocode.GetAreaName(parsedLatitude, parsedLongitude);
            }

            return Json(location, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusinessDetails(YelpBusiness business)
        {
            // var allBusinesses = queryBusiness.GetBusinessByCriteria(criteria);

            return View(business);
        }
    }
}