using System.Net.Http;

namespace kFriendly.Core.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
    }

    public interface IHTTPLogger : ILogger
    {
        void Log(HttpRequestMessage request);
        void Log(HttpResponseMessage response);
    }
}