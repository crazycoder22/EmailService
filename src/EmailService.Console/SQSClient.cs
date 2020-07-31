using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using EmailService.Common.Model;
using System.Net;

namespace EmailService.Console
{
    public class SQSClient
    {
        private AmazonSQSClient _sqsClient;
        private SqsClientConfig _sqsClientConfig;
        public SQSClient(SqsClientConfig sqsClientConfig)
        {
            _sqsClientConfig = sqsClientConfig;
            var amazonSqsConfig = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_sqsClientConfig.Region),
            };
            _sqsClient = new AmazonSQSClient(sqsClientConfig.AccessKey, sqsClientConfig.Secret, amazonSqsConfig);
        }

        public (string, string) ReadSQSQueue()
        {
            var request = new ReceiveMessageRequest()
            {
                QueueUrl = _sqsClientConfig.Url,
                VisibilityTimeout = 60,   // Number of seconds to process a message before it is returned to the queue
                MaxNumberOfMessages = 1,  // Max number of messages to return in a single read
                WaitTimeSeconds = 1       // Number of seconds to wait for a response when attempting to read a message
            };

            var message = _sqsClient.ReceiveMessageAsync(request).Result;

            if (message.HttpStatusCode != HttpStatusCode.OK || message.Messages.Count != 1)
            {
                return (string.Empty, string.Empty);
            }
            //var rsp = client.DeleteMessageAsync("https://sqs.us-west-2.amazonaws.com/350484936906/emailservice", message.Messages[0].ReceiptHandle).Result;
            return (message.Messages[0].ReceiptHandle, message.Messages[0].Body);
        }

        public void Acknowledge(string receipt)
        {
            var rsp = _sqsClient.DeleteMessageAsync(_sqsClientConfig.Url, receipt).Result;
        }
    }
}
