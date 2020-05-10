using System;
using Refit;
using ServerlessObservability.Services;

namespace ServerlessObservability.Providers
{
    public static class ExternalApiSingletonProvider
    {
        public static IExternalApi GetExternalApi() => ExternalApiApiLazy.Value;

        private static readonly Lazy<IExternalApi> ExternalApiApiLazy = new Lazy<IExternalApi>(
            () =>
            {
                var httpClient = HttpClientSingletonProvider.GetHttpClient();
                return RestService.For<IExternalApi>(httpClient);
            }
        );
    }
}
