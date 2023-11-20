using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using CXChange.Modules.Payment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterPaymentInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseNpgsql(connectionString, opt =>
                {
                    opt.MigrationsHistoryTable("ef_migrations_history", "payment");

                })
                .UseSnakeCaseNamingConvention();

            });
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork>("payment");
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICurrencyConfigRepository, CurrencyConfigRepository>();
            services.AddScoped<IBankCardRepository, BankCardRepository>();
            services.AddScoped<IShabaRepository, ShabaRepository>();
            services.AddScoped<IDepositRepository, DepositRepository>();
        }
    }
}
