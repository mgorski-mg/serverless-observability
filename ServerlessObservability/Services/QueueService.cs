using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using ServerlessObservability.Models;

namespace ServerlessObservability.Services
{
    public class QueueService
    {
        private readonly IAmazonSQS _sqsClient;

        public QueueService(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient;
        }

        public async Task AddMessageAsync(ItemMessage itemMessage, string queueUrl)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = JsonSerializer.Serialize(itemMessage)
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
