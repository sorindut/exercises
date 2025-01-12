using System.Net;

namespace RateLimiter.Contracts
{
    public interface IRateController
    {
        bool ValidateClient(IPAddress address);
    }
}
