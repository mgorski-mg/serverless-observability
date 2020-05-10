using System;
using System.Net.Http;
using Amazon.XRay.Recorder.Handlers.System.Net;
using ServerlessObservability.Configuration;

namespace ServerlessObservability.Providers
{
    public static class HttpClientSingletonProvider
    {
        public static HttpClient GetHttpClient() => HttpClientLazy.Value;

        private static readonly Lazy<HttpClient> HttpClientLazy = new Lazy<HttpClient>(
            () =>
            {
                var clientHandler = new HttpClientHandler();
                var httpClientXRayTracingHandler = new HttpClientXRayTracingHandler(clientHandler);
                return new HttpClient(httpClientXRayTracingHandler) { BaseAddress = new Uri(ConfigurationReader.GetExternalApiConfig().BaseUrl) };
            }
        );
    }
}
