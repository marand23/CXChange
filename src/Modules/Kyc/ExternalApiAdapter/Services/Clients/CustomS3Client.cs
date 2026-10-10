using Amazon.S3;

namespace CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients
{
    internal class CustomS3Client
    {
        public static AmazonS3Client S3Client()
        {
            var credentials = new Amazon.Runtime.BasicAWSCredentials("", "");
            var config = new AmazonS3Config { ServiceURL = "" };
            var client = new AmazonS3Client(credentials, config);
            return client;
        }
    }
}
