using kFriendly.Core.Models;
using kFriendly.Entities;
using System;
using System.Collections.Generic;

namespace kFriendly.Core.Interfaces
{
    public interface IQueryBusiness
    {
        List<YelpBusiness> GetBusinessByCriteria(SearchBusinessModel searchCriteria);
        YelpBusiness GetBusinessById(string businessId);

    }
}
