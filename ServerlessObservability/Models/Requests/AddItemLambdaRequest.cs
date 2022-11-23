using System;

namespace ServerlessObservability.Models.Requests
{
    public class AddItemLambdaRequest
    {
        public string Message { get; }

        public AddItemLambdaRequest(string message)
        {
            Message = message;
        }

        public NewItem ToNewItem() => new(Guid.NewGuid(), Message);
    }
}
