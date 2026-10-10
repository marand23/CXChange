using CXChange.AdminAPI.Modules.Kyc.Maps;
using CXChange.AdminAPI.Modules.Kyc.Requests;
using CXChange.AdminAPI.Modules.UserAccess.Maps;
using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;
using CXChange.AdminAPI.Modules.UserAccess.Requests.User;
using CXChange.API.Modules.UserManagment.Maps;
using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Kyc.Administrator.Application;
using CXChange.Modules.Kyc.Administrator.Application.CommandFilters;
using CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet;
using CXChange.Modules.UserAccess.Administrator.Application;
using CXChange.Modules.UserAccess.Administrator.Application.Queries.Users;
using MassTransit;
using MassTransit.Internals;
namespace CXChange.AdminAPI
{
    public static class DependencyInjection
    {
        public static void RegisterApi(this IServiceCollection services)
        {
            services.AddSingleton<IMapper<FilterItemRequest<string>, FilterItemQuery<string>>, MapFilterItemRequestToFilterItemQuery<string>>();
            services.AddSingleton<IMapper<FilterItemRequest<bool>, FilterItemQuery<bool>>, MapFilterItemRequestToFilterItemQuery<bool>>();
            services.AddSingleton<IMapper<GetUsersRequest, GetUsers>, MapUsersReqToUsers>();
            services.AddSingleton<IMapper<KycRequests, GetKycRequests>, MapKycRequestToKycRequestQuery>();




        }
        public static void RegisterMAssTransit(this IServiceCollection services, IWebHostEnvironment environment)
        {

            services.AddMassTransit(x =>
            {
                // x.AddConsumer<sendemailcosumer> ();




                x.UsingInMemory((context, cfg) =>
                {
                    //cfg.ConfigureEndpoints(context);
                    //cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                    //cfg.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(20)));
                });
                //x.UsingRabbitMq((context, cfg) =>
                //{
                //    cfg.Host("localhost", "/", h =>
                //    {
                //        h.Username("guest");
                //        h.Password("guest");
                //    });
                //    //cfg.ReceiveEndpoint("user-registered",c =>
                //    //{

                //    //    c.UseMessageRetry(r =>
                //    //    {
                //    //        r.Immediate(2);

                //    //        r.Handle<DbUpdateException>();
                //    //        r.Ignore<NotImplementedException>();
                //    //    });



                //    //});
                //    cfg.ConfigureEndpoints(context);




                //});
            });



        }
        public static void RegisterMediator(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddMediator(cfg =>
            {
                cfg.AddUserAccessApplicationMediator();
                cfg.AddKycAdminApplicationMediator();
                cfg.ConfigureMediator((context, mcfg) =>
                {


                    mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context, x =>
                    {
                        x.Include(type => type.HasInterface<IPersistable>());
                    });


                });
            });
        }
    }
}
