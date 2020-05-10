using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using ServerlessObservability.Configuration;
using ServerlessObservability.Models;

namespace ServerlessObservability.Services
{
    public class QueueService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ItemsQueueConfig _config;

        public QueueService(IAmazonSQS sqsClient, ItemsQueueConfig config)
        {
            _sqsClient = sqsClient;
            _config = config;
        }

        public async Task AddMessageAsync(ItemMessage itemMessage)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _config.QueueUrl,
                MessageBody = JsonSerializer.Serialize(itemMessage)
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
