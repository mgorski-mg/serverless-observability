using System;

namespace ServerlessObservability.Models.Requests
{
    public class AddItemLambdaRequest
    {
        public string Message { get; set; }

        public AddItemLambdaRequest() {}

        public AddItemLambdaRequest(string message)
        {
            Message = message;
        }

        public NewItem ToNewItem() => new NewItem(Guid.NewGuid(), Message);
    }
}
