using CXChange.Modules.Kyc.Application.ExternalApiServices;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;

namespace CXChange.Modules.Kyc.Infrastructure.Jobs
{
    internal class RemoveStaleKycRequestsJob
    {
        KycDbContext _context;
        IEnumerable<IKycPhotoActions> _kycPhotoActions;

        public RemoveStaleKycRequestsJob(KycDbContext context, IEnumerable<IKycPhotoActions> kycPhotoActions)
        {
            _context = context;
            _kycPhotoActions = kycPhotoActions;

        }
        public async Task Remove()
        {
            var staleKycRequests = _context.KycRequests.Where(x => x.KycRequestStatus == Domain.KycRequest.Enums.KycRequestStatus.Stale).ToList();
            foreach (var kyc in staleKycRequests)
            {
                try
                {
                    var kycStorage = _kycPhotoActions.FirstOrDefault(x => x.StorageType == kyc.StorageType);
                    await kycStorage.DeleteKycPhoto(kyc.KycPhotoId);
                    _context.Remove(kyc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception in removing");
                }
            }
            await _context.SaveChangesAsync();
        }


    }
}
