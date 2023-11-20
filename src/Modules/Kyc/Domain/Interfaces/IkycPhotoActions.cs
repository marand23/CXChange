using CXChange.Modules.Kyc.Domain.User.Enums;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.Domain.Interfaces
{
    public interface IkycPhotoActions
    {
        StorageTypeEnum StorageType { get; }
        Task<string> UploadKycPhoto(IFormFile file);
        Task<byte[]> DownloadKycPhoto(string id);
        Task<bool> DeleteKycPhoto(string id);
    }
}
