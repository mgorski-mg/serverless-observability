using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Lambda.SQSEvents;
using ServerlessObservability.Configuration;
using ServerlessObservability.Functions.Base;
using ServerlessObservability.Models;
using ServerlessObservability.Models.Requests;
using ServerlessObservability.Providers;
using ServerlessObservability.Repositories;

namespace ServerlessObservability.Functions
{
    public class UpdateItemLambdaV2 : LoggingUnhandledExceptionNoResultLambda<SQSEvent>
    {
        protected override async Task HandleAsync(SQSEvent sqsEvent)
        {
            var itemMessage = ExtractItemMessage(sqsEvent);
            var s3Repository = GetS3Repository();

            var item = await s3Repository.GetNewItemAsync(itemMessage.ItemId);
            var doneItem = item.Finish(DateTime.UtcNow);
            Logger.Log("New item finished", "INFO");
            await s3Repository.PutDoneItemAsync(doneItem);
            Logger.Log("Done item saved", "INFO");

            await AwsClientsSingletonsProvider.GetLambdaClient()
                                              .InvokeAsync(
                                                   new InvokeRequest
                                                   {
                                                       InvocationType = InvocationType.Event,
                                                       FunctionName = ConfigurationReader.GetNotifyLambdaName(),
                                                       Payload = JsonSerializer.Serialize(new AddItemLambdaRequest("test connection V2"))
                                                   }
                                               );
        }

        private static ItemMessage ExtractItemMessage(SQSEvent sqsEvent)
        {
            var sqsMessage = sqsEvent.Records.Single();

            var itemMessage = JsonSerializer.Deserialize<ItemMessage>(sqsMessage.Body);
            return itemMessage!;
        }

        private static S3Repository GetS3Repository()
        {
            var s3Client = AwsClientsSingletonsProvider.GetS3Client();
            var s3Config = ConfigurationReader.GetS3Config();
            return new S3Repository(s3Client, s3Config);
        }
    }
}
