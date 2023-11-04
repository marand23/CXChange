using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.Application.ExternalApiServices
{
    public interface IKycPhotoActions
    {
        StorageTypeEnum StorageType { get; }
        Task<string> UploadKycPhoto(IFormFile file);
        Task<byte[]> DownloadKycPhoto(string id);
        Task<bool> DeleteKycPhoto(string id);


    }

}
