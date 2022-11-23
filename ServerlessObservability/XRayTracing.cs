using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Core.Internal.Utils;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

namespace ServerlessObservability
{
    public class XRayTracing
    {
        public static string TraceId => AWSXRayRecorder.Instance.TraceContext.GetEntity().TraceId;

        private Segment? _lambdaSegment;

        private readonly decimal _startTime;

        public XRayTracing()
        {
            _startTime = DateTime.UtcNow.ToUnixTimeSeconds();
            AWSSDKHandler.RegisterXRayForAllServices();
            // AWSXRayRecorder.RegisterLogger(LoggingOptions.Console); // uncomment to add XRAy debug logs to CloudWatch
        }

        public void RewriteTraceContext(SQSEvent.SQSMessage sqsMessage, ILambdaContext lambdaContext)
        {
            var traceHeader = TraceHeader.FromString(sqsMessage.Attributes["AWSTraceHeader"]);
            var lambdaSegmentParentId = traceHeader.ParentId;
            var lambdaSegmentId = Entity.GenerateId();
            traceHeader.ParentId = lambdaSegmentId;

            Environment.SetEnvironmentVariable(AWSXRayRecorder.LambdaTraceHeaderKey, traceHeader.ToString());

            _lambdaSegment = new Segment(lambdaContext.FunctionName, traceHeader.RootTraceId)
            {
                Id = lambdaSegmentId,
                ParentId = lambdaSegmentParentId,
                Sampled = traceHeader.Sampled,
                Origin = "AWS::Lambda::Function",
                Aws =
                {
                    { "account_id", lambdaContext.InvokedFunctionArn.Split(":")[4] },
                    { "function_arn", lambdaContext.InvokedFunctionArn },
                    { "resource_names", new List<string> { lambdaContext.FunctionName } }
                }
            };

            _lambdaSegment.SetStartTime(_startTime);
        }

        public void EmitLambdaSegmentsIfNeeded()
        {
            if (_lambdaSegment != null)
            {
                _lambdaSegment.SetEndTimeToNow();
                AWSXRayRecorder.Instance.Emitter.Send(_lambdaSegment);
            }
        }
    }
}
