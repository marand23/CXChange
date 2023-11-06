using CXChange.Modules.Kyc.Administrator.Application.Commands.KycPhotoDownlaod;
using CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestApprove;
using CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestReject;
using CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Kyc.Administrator.Application
{
    public static class DependencyInjection
    {
        public static void RegisterKycAdminApplication(this IServiceCollection services)
        {

        }
        public static void AddKycAdminApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetKycRequestsHandler>();
            cfg.AddConsumer<ApproveKycRequestHandler>();
            cfg.AddConsumer<RejectKycRequestHandler>();
            cfg.AddConsumer<DownloadKycPhotoHandler>();

        }
    }
}
