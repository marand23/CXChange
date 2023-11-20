using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.Domain.User.Enums;
using Google;
using Google.Apis.Download;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients
{
    internal class GoogleDriveActions : IkycPhotoActions
    {
        public StorageTypeEnum StorageType => StorageTypeEnum.GoogleDrive;

        public Task<byte[]> DownloadKycPhoto(string id)
        {
            var service = GoogleDriveService.GetService();


            var file = service.Files.Get(id);
            using (var stream = new MemoryStream())
            {
                file.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Completed:

                            break;
                        case DownloadStatus.Failed:

                            break;
                    }
                };
                file.Download(stream);
                return Task.FromResult(stream.ToArray());
            }


        }

        public async Task<string> UploadKycPhoto(IFormFile file)
        {

            var service = GoogleDriveService.GetService();
            using (Stream stream = file.OpenReadStream())
            {
                var driveFile = new Google.Apis.Drive.v3.Data.File();
                driveFile.Name = file.Name;
                driveFile.MimeType = file.ContentType;


                var request = service.Files.Create(driveFile, stream, "image/jpeg");
                request.Fields = "id";
                var response = await request.UploadAsync();
                if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                    throw response.Exception;
                return request.ResponseBody.Id;

            }
        }
        public async Task<bool> DeleteKycPhoto(string id)
        {
            try
            {
                var service = GoogleDriveService.GetService();
                var request = service.Files.Delete(id);
                var response = await request.ExecuteAsync();
                return true;
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 404) { return true; }
                throw;
            }


        }
    }

}
