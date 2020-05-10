using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using ServerlessObservability.Configuration;
using ServerlessObservability.Models;

namespace ServerlessObservability.Repositories
{
    public class S3Repository
    {
        private const string JsonContentType = "application/json";

        private readonly IAmazonS3 _amazonS3Client;
        private readonly S3Config _config;

        public S3Repository(IAmazonS3 amazonS3Client, S3Config config)
        {
            _amazonS3Client = amazonS3Client;
            _config = config;
        }

        public async Task PutNewItemAsync(NewItem item) => await PutItemAsync(item.Id, item.Type, JsonSerializer.Serialize(item));

        public async Task PutDoneItemAsync(DoneItem item) => await PutItemAsync(item.Id, item.Type, JsonSerializer.Serialize(item));

        private async Task PutItemAsync(Guid itemId, ItemType itemType, string itemJson)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _config.BucketName,
                Key = $"{itemType}/{itemId}",
                ContentType = JsonContentType,
                ContentBody = itemJson
            };

            await _amazonS3Client.PutObjectAsync(putObjectRequest);
        }

        public async Task<NewItem> GetNewItemAsync(Guid itemId)
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = _config.BucketName,
                Key = $"{ItemType.New}/{itemId}"
            };

            var rawS3Response = await _amazonS3Client.GetObjectAsync(getObjectRequest);
            await using var itemStream = rawS3Response.ResponseStream;
            var streamReader = new StreamReader(itemStream);
            var item = JsonSerializer.Deserialize<NewItem>(streamReader.ReadToEnd());

            return item;
        }
    }
}
