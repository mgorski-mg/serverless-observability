dotnet lambda deploy-serverless `
    --configuration Release `
    --region eu-west-1 `
    --stack-name serverless-observability `
    --s3-bucket [s3-bucket-name] `
    --s3-prefix serverless-observability/ `
    --template application.yaml `
    --tags "service-name=sqs-lambda-throughput-limit";