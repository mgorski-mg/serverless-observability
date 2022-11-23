# serverless-observability
Sample app showing how to monitor systems with the serverless approach.

Covered topics:
- structured logs
- AWS X-Ray
- correlation id - tracking the path of the request
- AWS X-Ray SQS to Lambda
  - AWS X-Ray with Lambda and SQS - already fixed by AWS - UpdateItemLambda
  -  automatic tracking of SQS to Lambda - [announcement](https://aws.amazon.com/about-aws/whats-new/2022/11/aws-x-ray-trace-linking-event-driven-applications-amazon-sqs-lambda/) - UpdateItemLambdaV2