using CXChange.API.Modules.Payment.GateWayAdapter;
using CXChange.API.Modules.Payment.Maps;
using CXChange.BuildingBlocks.Application.CommandFilters;
using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Kyc.Application;
using CXChange.Modules.Kyc.Application.CommandFilters;
using CXChange.Modules.Payment.Application;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using CXChange.Modules.UserAccess.Application;
using MassTransit;
using MassTransit.Internals;
using Microsoft.AspNetCore.Mvc;
using static CXChange.API.Modules.Payment.Requests;
namespace CXChange.API
{
    public class UrlOptions
    {
        public string Url { get; set; }
    }
    public static class DependencyInjection
    {

        public static void RegisterAPI(this IServiceCollection services, Action<UrlOptions> opt)
        {

            services.AddKeyedScoped<IGateWayAdapter, ZibalAdapter>("zibal");
            services.AddKeyedScoped<IGateWayAdapter, NextAdapter>("next");
            services.AddSingleton<IMapper<ZibalGateWayRequest, VerifyTomanDeposit>, MapZibalCallBackToConfirmDeposit>();
            services.AddOptions<UrlOptions>();
            services.Configure<UrlOptions>(opt);
            services.Configure<ApiBehaviorOptions>(o =>
            {


                //o.InvalidModelStateResponseFactory = actionContext =>
                //    new UnprocessableEntityObjectResult(new ApiValidationErrorModel { message = "ValidationError", errors = actionContext.ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.FirstOrDefault().ErrorMessage), success = false }); ;
            });

            //services.Scan(scan => scan
            //.FromCallingAssembly()
            //.AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
            //.AsImplementedInterfaces()
            //.WithSingletonLifetime());








        }

        public static void RegisterMAssTransit(this IServiceCollection services, IWebHostEnvironment environment)
        {

            services.AddMassTransit(x =>
            {
                // x.AddConsumer<sendemailcosumer>();
                //x.AddEntityFrameworkOutbox<UserManagementDbContext>(o =>
                //{
                //    configure which database lock provider to use(Postgres, SqlServer, or MySql)
                //    o.UsePostgres();
                //    enable the bus outbox
                //    o.UseBusOutbox();
                //});


                x.AddKycApplicationBus();
                x.AddUserManagmentApplicationBus();
                x.AddPaymentApplicationBus();
                x.UsingInMemory((context, cfg) =>
                {

                    cfg.ConfigureEndpoints(context);

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
                cfg.AddKycApplicationMediator();
                cfg.RegisterPaymentApplicationMediator();

                cfg.ConfigureMediator((context, mcfg) =>
                {
                    mcfg.UseConsumeFilter(typeof(LoggingFilter<>), context);

                    mcfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x =>
                    {
                        x.Include(type => type.HasInterface<IValidatable>());

                    });

                    mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context, x =>
                    {
                        x.Include(type => type.HasInterface<IPersistable>());
                    });
                    mcfg.UseConsumeFilter(typeof(CXChange.Modules.UserAccess.Application.CommandFilters.UnitOfWorkFilter<>), context, x =>
                    {
                        x.Include(type => type.HasInterface<IPersistable>());
                    });

                });


            });
        }
    }
}
