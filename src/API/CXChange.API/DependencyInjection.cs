using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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
            services.AddMassTransit(x =>
            {

                x.AddEntityFrameworkOutbox<UserManagementDbContext>(o =>
                {
                    // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                    o.UseSqlServer();

                    // enable the bus outbox
                    o.UseBusOutbox();
                    o.QueryDelay = TimeSpan.FromHours(1);

                });
             
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
                    cfg.ReceiveEndpoint("user-registered",c =>
                    {
                       
                        c.UseMessageRetry(r =>
                        {
                            r.Immediate(2);

                            r.Handle<DbUpdateException>();
                            r.Ignore<NotImplementedException>();
                        });
                       
                      

                    });
                    cfg.ConfigureEndpoints(context);




                });
            });



        }


    }
}
