using System;
using Amazon.Lambda;
using Amazon.S3;
using Amazon.SQS;

namespace ServerlessObservability.Providers
{
    public static class AwsClientsSingletonsProvider
    {
        public static IAmazonS3 GetS3Client() => S3ClientLazy.Value;
        public static IAmazonSQS GetSqsClient() => SqsClientLazy.Value;
        public static IAmazonLambda GetLambdaClient() => LambdaClientLazy.Value;

        private static readonly Lazy<IAmazonS3> S3ClientLazy = new(() => new AmazonS3Client());
        private static readonly Lazy<IAmazonSQS> SqsClientLazy = new(() => new AmazonSQSClient());
        private static readonly Lazy<IAmazonLambda> LambdaClientLazy = new(() => new AmazonLambdaClient());
    }
}
