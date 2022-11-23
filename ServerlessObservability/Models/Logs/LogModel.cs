using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerlessObservability.Models.Logs
{
    public class LogModel
    {
        [JsonPropertyName("correlation-id")]
        public string CorrelationId { get; set; }

        [JsonPropertyName("xray-trace-id")]
        public string XrayTraceId => " " + CorrelationId + " ";

        [JsonPropertyName("function-name")]
        public string FunctionName { get; set; }

        [JsonPropertyName("log-group")]
        public string LogGroup { get; set; }

        [JsonPropertyName("log-stream")]
        public string LogStream { get; set; }

        [JsonPropertyName("log-message")]
        public string LogMessage { get; set; }

        [JsonPropertyName("log-level")]
        public string LogLevel { get; set; }

        [JsonPropertyName("environment-type")]
        public string EnvironmentType { get; set; }

        [JsonPropertyName("service-name")]
        public string ServiceName { get; set; }

        [JsonPropertyName("aws-request-id")]
        public string AwsRequestId { get; set; }

        [JsonPropertyName("exception")]
        public string? Exception { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
