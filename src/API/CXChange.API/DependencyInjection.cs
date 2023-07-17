using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CXChange.Modules.UserAccess.Application;

namespace CXChange.API
{
    public static class DependencyInjection
    {
        public static void RegisterAPI(this IServiceCollection services, IWebHostEnvironment environment)
        {



            AddMAssTransit(services, environment);

        }
        private static void AddMAssTransit(IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddMediator(cfg =>
            {
                cfg.AddUserManagmentApplicationMediator();



            });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<sendemailcosumer> ();


             
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


    }
}
