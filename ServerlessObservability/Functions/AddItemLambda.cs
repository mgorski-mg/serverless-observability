using System.Threading.Tasks;
using ServerlessObservability.Configuration;
using ServerlessObservability.Functions.Base;
using ServerlessObservability.Models;
using ServerlessObservability.Models.Requests;
using ServerlessObservability.Providers;
using ServerlessObservability.Repositories;
using ServerlessObservability.Services;

namespace ServerlessObservability.Functions
{
    public class AddItemLambda : LoggingUnhandledExceptionNoResultLambda<AddItemLambdaRequest>
    {
        protected override async Task HandleAsync(AddItemLambdaRequest lambdaRequest)
        {
            var newItem = lambdaRequest.ToNewItem();

            var s3Client = AwsClientsSingletonsProvider.GetS3Client();
            var s3Config = ConfigurationReader.GetS3Config();
            var s3Repository = new S3Repository(s3Client, s3Config);
            await s3Repository.PutNewItemAsync(newItem);
            Logger.Log("New item saved", "INFO");

            var sqsClient = AwsClientsSingletonsProvider.GetSqsClient();
            var queueService = new QueueService(sqsClient, ConfigurationReader.GetSqsConfig());
            await queueService.AddMessageAsync(new ItemMessage(newItem.Id));
            Logger.Log("New item added event sent", "INFO");
        }
    }
}
