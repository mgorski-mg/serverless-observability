using System;
using System.Text.Json.Serialization;

namespace ServerlessObservability.Models.ExternalApi
{
    public class ExternalApiTime
    {
        [JsonPropertyName("currentDateTime")]
        public DateTime CurrentDateTime { get; set; }
    }
}
