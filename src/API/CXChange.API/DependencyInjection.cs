using CXChange.API.Modules.UserManagment.Maps;
using CXChange.API.Modules.UserManagment.Requests.User;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application;
using CXChange.Modules.UserAccess.Application.Queries.Users;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

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


            services.AddOptions<UrlOptions>();
            services.Configure<UrlOptions>(opt);
            services.Configure<ApiBehaviorOptions>(o =>
            {


                //o.InvalidModelStateResponseFactory = actionContext =>
                //    new UnprocessableEntityObjectResult(new ApiValidationErrorModel { message = "ValidationError", errors = actionContext.ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.FirstOrDefault().ErrorMessage), success = false }); ;
            });
            services.AddSingleton<IMapper<GetUsersRequest, GetUsers>, MapUsersReqToUsers>();
            services.AddSingleton<IMapperService, MapService>();

        }

        public static void RegisterMAssTransit(this IServiceCollection services, IWebHostEnvironment environment)
        {

            services.AddMassTransit(x =>
            {
                // x.AddConsumer<sendemailcosumer> ();


                //  x.UsingInMemory();
                // configure bus
                //x.UsingInMemory((context, cfg) =>
                //{
                //    cfg.ConfigureEndpoints(context);
                //    cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                //    cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromSeconds(20)));
                //});
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    //cfg.ReceiveEndpoint("user-registered",c =>
                    //{

                    //    c.UseMessageRetry(r =>
                    //    {
                    //        r.Immediate(2);

                    //        r.Handle<DbUpdateException>();
                    //        r.Ignore<NotImplementedException>();
                    //    });



                    //});
                    cfg.ConfigureEndpoints(context);




                });
            });



        }
        public static void RegisterMediator(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddMediator(cfg =>
            {
                cfg.AddUserAccessApplicationMediator();
            });
        }

    }
}
