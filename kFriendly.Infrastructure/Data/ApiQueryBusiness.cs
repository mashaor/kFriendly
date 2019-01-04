using kFriendly.Core.Interfaces;
using kFriendly.Core.Models;
using kFriendly.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kFriendly.Infrastructure.Data
{
    class ApiQueryBusiness : IQueryBusiness
    {
        public List<YelpBusiness> GetBusinessByCriteria(SearchBusinessModel searchCriteria)
        {
            throw new NotImplementedException();
        }

        public YelpBusiness GetBusinessById(string businessId)
        {
            throw new NotImplementedException();
        }
    }
}
