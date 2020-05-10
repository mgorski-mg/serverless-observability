using System.Threading.Tasks;
using Refit;
using ServerlessObservability.Models.ExternalApi;

namespace ServerlessObservability.Services
{
    public interface IExternalApi
    {
        [Get("/api/json/utc/now")]
        Task<ExternalApiTime> GetTimeAsync();
    }
}
