using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using ServerlessObservability.Services;

namespace ServerlessObservability.Functions.Base
{
    public abstract class LoggingUnhandledExceptionNoResultLambda<TRequest>
    {
        protected MyLogger Logger;
        protected XRayTracing XRayTracing;
        protected ILambdaContext LambdaContext;

        public async Task InvokeAsync(TRequest request, ILambdaContext lambdaContext)
        {
            XRayTracing = new XRayTracing();

            LambdaContext = lambdaContext;
            Logger = new MyLogger(LambdaContext);

            try
            {
                await HandleAsync(request);
            }
            catch (Exception ex)
            {
                Logger.Log("Unhandled exception occured.", "ERROR", ex);
                throw;
            }
            finally
            {
                XRayTracing.EmitLambdaSegmentsIfNeeded();
            }
        }

        protected abstract Task HandleAsync(TRequest request);
    }
}
