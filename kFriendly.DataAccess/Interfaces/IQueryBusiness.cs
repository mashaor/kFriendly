using kFriendly.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kFriendly.Core.Interfaces
{
    public interface IQueryBusiness
    {
        Task<KFBusinessModel> GetBusinessByCriteria(string term, string location);
        BusinessDetailsResponse GetBusinessById(string businessId);
        Task<List<string>> Autocomplete(SearchRequest searchCriteria);
    }
}
