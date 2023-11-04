using CXChange.Modules.Kyc.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;
using CXChange.Modules.Kyc.Infrastructure.Jobs;
using CXChange.Modules.Kyc.Infrastructure.Repsitories;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Kyc.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterKycInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<KycDbContext>(options =>
           {

               options.UseNpgsql(connectionString, opt =>
               {
                   opt.MigrationsHistoryTable("ef_migrations_history", "kyc");
               })
               .UseSnakeCaseNamingConvention();

           });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IKycRequestRepository, KycRequestRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<RemoveStaleKycRequestsJob>();
        }
        public static void RegisterKycInfrastructureJobs()
        {

            RecurringJob.AddOrUpdate<RemoveStaleKycRequestsJob>("fs", x => x.Remove(), Cron.Minutely);
        }

    }
}
