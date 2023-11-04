using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients
{
    internal class GoogleDriveService
    {
        public static DriveService GetService()
        {

            GoogleCredential credential = GoogleCredential.FromFile("C:\\Users\\USER\\Downloads\\focused-sound-351917-f288e4535b62.json").CreateScoped(DriveService.Scope.DriveFile);
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;

        }
    }
}
