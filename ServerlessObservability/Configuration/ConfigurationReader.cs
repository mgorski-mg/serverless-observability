using System;
using Microsoft.Extensions.Configuration;

namespace ServerlessObservability.Configuration
{
    public static class ConfigurationReader
    {
        private static IConfigurationRoot Config => ConfigLazy.Value;

        private static readonly Lazy<IConfigurationRoot> ConfigLazy = new(InitConfig);

        private static IConfigurationRoot InitConfig()
        {
            var configurationBuilder = new ConfigurationBuilder()
               .AddEnvironmentVariables();

            return configurationBuilder.Build();
        }

        public static ItemsQueueConfig GetSqsConfig()
            => new()
            {
                QueueUrl = Config["QueueUrl"]!,
                QueueV2Url = Config["QueueV2Url"]!
            };


        public static S3Config GetS3Config() => new() { BucketName = Config["BucketName"]! };

        public static string GetNotifyLambdaName() => Config["NotifyLambdaName"]!;

        public static string GetExternalApiUrl() => Config["ExternalApiUrl"]!;
    }
}
