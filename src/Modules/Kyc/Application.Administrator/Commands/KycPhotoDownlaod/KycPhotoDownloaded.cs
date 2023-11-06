namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycPhotoDownlaod
{
    public record KycPhotoDownloaded
    {
        public byte[] Photo { get; init; }
        public string NationalId { get; init; }
    }
}
