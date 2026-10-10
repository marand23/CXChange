using CXChange.BuildingBlocks.Application.Command;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycPhotoDownlaod
{
    public record DownloadKycPhoto : ICommandBase
    {
        public Guid UserId { get; init; }
    }
}
