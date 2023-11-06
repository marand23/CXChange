using Amazon.S3.Model;
using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients
{
    internal class CustomS3Actions : IkycPhotoActions
    {
        private readonly string Bucket = "testatro";
        public StorageTypeEnum StorageType => StorageTypeEnum.CustomS3;

        public async Task<bool> DeleteKycPhoto(string id)
        {
            var s3Client = CustomS3Client.S3Client();
            var request = new Amazon.S3.Model.DeleteObjectRequest
            {
                BucketName = Bucket,
                Key = id
            };
            var resu = await s3Client.DeleteObjectAsync(request);
            Console.WriteLine(JsonConvert.SerializeObject(resu, Formatting.Indented));

            return true;

        }

        public async Task<byte[]> DownloadKycPhoto(string id)
        {

            var s3Client = CustomS3Client.S3Client();
            var request = new Amazon.S3.Model.GetObjectRequest
            {
                BucketName = Bucket,
                Key = id
            };
            var response = await s3Client.GetObjectAsync(request);
            using (Stream responseStream = response.ResponseStream)
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = new byte[81920];
                int bytesRead;
                while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead);
                }


                byte[] fileBytes = memoryStream.ToArray();
                return fileBytes;
            }

        }

        public async Task<string> UploadKycPhoto(IFormFile file)
        {
            var s3Client = CustomS3Client.S3Client();
            using (Stream stream = file.OpenReadStream())
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    Key = Guid.NewGuid().ToString(),
                    BucketName = Bucket,
                    InputStream = stream
                };
                PutObjectResponse reponse = await s3Client.PutObjectAsync(request);

                return request.Key;
            }
        }
    }
}
