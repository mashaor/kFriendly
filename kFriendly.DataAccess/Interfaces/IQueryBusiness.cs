using kFriendly.Core.Models;

namespace kFriendly.Core.Interfaces
{
    public interface IQueryBusiness
    {
        BusinessSearchResponse GetBusinessByCriteria(SearchRequest searchCriteria);
        BusinessDetailsResponse GetBusinessById(string businessId);
    }
}
