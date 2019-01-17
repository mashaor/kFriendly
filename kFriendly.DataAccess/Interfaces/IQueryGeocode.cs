using System.Threading.Tasks;

namespace kFriendly.Core.Interfaces
{
    public interface IQueryGeocode
    {
        Task<string> GetAreaName(double latitude, double longitude);
    }
}
