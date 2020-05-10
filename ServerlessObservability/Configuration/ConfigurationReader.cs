using System;
using Microsoft.Extensions.Configuration;

namespace ServerlessObservability.Configuration
{
    public static class ConfigurationReader
    {
        private static IConfigurationRoot Config => ConfigLazy.Value;

        private static readonly Lazy<IConfigurationRoot> ConfigLazy = new Lazy<IConfigurationRoot>(InitConfig);

        private static IConfigurationRoot InitConfig()
        {
            var stackName = Environment.GetEnvironmentVariable("StackName");
            var configurationBuilder = new ConfigurationBuilder()
                                      .AddEnvironmentVariables()
                                      .AddSystemsManager(
                                           configSource =>
                                           {
                                               configSource.Path = $"/{stackName}";
                                               configSource.ReloadAfter = TimeSpan.FromMinutes(5);
                                           }
                                       );

            return configurationBuilder.Build();
        }

        public static ItemsQueueConfig GetSqsConfig() => new ItemsQueueConfig { QueueUrl = Config["QueueUrl"] };


        public static S3Config GetS3Config() => new S3Config { BucketName = Config["BucketName"] };

        public static string GetNotifyLambdaName() => Config["NotifyLambdaName"];

        public static ExternalApiConfig GetExternalApiConfig()
        {
            var externalApiConfig = new ExternalApiConfig();
            Config.GetSection("ExternalApiConfig").Bind(externalApiConfig);
            return externalApiConfig;
        }
    }
}
