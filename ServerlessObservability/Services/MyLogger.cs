using System;
using Amazon.Lambda.Core;
using ServerlessObservability.Models.Logs;

namespace ServerlessObservability.Services
{
    public class MyLogger
    {
        private readonly ILambdaContext _lambdaContext;

        public MyLogger(ILambdaContext lambdaContext)
        {
            _lambdaContext = lambdaContext;
        }

        public void Log(string logMessage, string logLevel, Exception? exception = null)
        {
            var logModel = new LogModel
            {
                CorrelationId = XRayTracing.TraceId,
                FunctionName = _lambdaContext.FunctionName,
                ServiceName = "serverless-observability",
                EnvironmentType = "dev",
                LogLevel = logLevel,
                LogMessage = logMessage,
                AwsRequestId = _lambdaContext.AwsRequestId,
                LogGroup = _lambdaContext.LogGroupName,
                LogStream = _lambdaContext.LogStreamName,
                Exception = exception?.ToString()
            };

            var json = logModel.ToJson();

            _lambdaContext.Logger.Log(json);
        }
    }
}
