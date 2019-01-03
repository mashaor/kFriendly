using kFriendly.Core;
using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
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
        public ActionResult Search(SearchBusinessModel criteria)
        {
            var allBusinesses = queryBusiness.GetBusinessByCriteria(criteria);
            return View("BusinessSearchResults", allBusinesses);
        }

        public ActionResult BusinessDetails(YelpBusiness business)
        {
           // var allBusinesses = queryBusiness.GetBusinessByCriteria(criteria);

            return View(business);
        }
    }
}