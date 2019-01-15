using kFriendly.Core;
using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
using kFriendly.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Search(string term, string location)
        {
            SearchRequest searchCriteria = new SearchRequest();
            searchCriteria.Term = term;
            searchCriteria.Location = location;

            BusinessSearchResponse allBusinesses = queryBusiness.GetBusinessByCriteria(searchCriteria);
            return View("BusinessSearchResults", allBusinesses);
        }

        [HttpPost]
        public ActionResult Autocomplete(string term,string latitude, string longitude)
        {
            var items = new[] { "Apple", "Pear", "Banana", "Pineapple", "Peach" };

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusinessDetails(YelpBusiness business)
        {
           // var allBusinesses = queryBusiness.GetBusinessByCriteria(criteria);

            return View(business);
        }
    }
}