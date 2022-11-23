using System.Threading.Tasks;
using ServerlessObservability.Functions.Base;
using ServerlessObservability.Models.Requests;
using ServerlessObservability.Providers;

namespace ServerlessObservability.Functions
{
    public class NotifyLambda : LoggingUnhandledExceptionNoResultLambda<AddItemLambdaRequest>
    {
        protected override async Task HandleAsync(AddItemLambdaRequest lambdaRequest)
        {
            var time = await ExternalApiSingletonProvider.GetExternalApi().GetTimeAsync();
            Logger.Log($"Notified about {lambdaRequest.Message} in {time.CurrentDateTime}", "INFO");
        }
    }
}
