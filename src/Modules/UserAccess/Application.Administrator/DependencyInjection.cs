using CXChange.Modules.UserAccess.Application.Queries.Users;
using MassTransit;

namespace CXChange.Modules.UserAccess.Administrator.Application
{
    public static class DependencyInjection
    {
        public static void AddUserAccessApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetUsersQueryHandler>();


        }
    }
}
