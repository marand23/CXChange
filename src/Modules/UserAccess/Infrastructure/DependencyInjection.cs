using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Infrastructure.Authentication.AuthenticationHandlers;
using CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore;
using CXChange.Modules.UserAccess.Infrastructure.Authorization.CodeTypeAuthorization;
using CXChange.Modules.UserAccess.Infrastructure.CustomUserDataProtectionProviders;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserAccess.Infrastructure.Identity;
using CXChange.Modules.UserAccess.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    public static class DependencyInjection
    {


        public static void RegisterUserAccessInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion, b =>
                {
                    b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}.{entity}");
                    b.MigrationsHistoryTable("__EFMigrationsHistory", "UserAccess");
                });
                options.UseSnakeCaseNamingConvention();










            });



            services.AddIdentity<AppUser, IdentityRole>()
             .AddDefaultTokenProviders()
             .AddCustomTokenProvider()

             .AddUserStore<CustomUSerStore>()
            .AddEntityFrameworkStores<UserManagementDbContext>();
            services.Configure<IdentityOptions>(options =>
            {

                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);



            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CustomAuthOptions.SchemeName;
                options.DefaultChallengeScheme = "CustomScheme";

            })

                .AddScheme<CustomAuthOptions, CustomAuthenticationHandler>(
                CustomAuthOptions.SchemeName, opts =>
                {
                    opts.ExpireTimeSpan = TimeSpan.FromDays(4);
                    opts.SessionStore = new CustomDatabaseTicketStore(services);






                }
                )
               ;
            services.AddAuthorization(options =>
            {
                options.AddPolicy("codeTypePolicy", policy =>
                {
                    policy.AddRequirements(new CodeRequirement(CodeType.GetCodeTypeByName));
                });
            });

            services.AddSingleton<IAuthorizationHandler, CodeTypeAuthorizationHandler>();

            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IExchangeUserManager, ExchangeUserManager>();
            services.AddScoped<IExchangeSignInManager, ExchangeSignInManager>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();









        }
        public static IdentityBuilder AddCustomTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var customProvider = typeof(CustomTokenProvider<>).MakeGenericType(userType);

            return builder.AddTokenProvider("CusotmTokenProvider", customProvider);
        }
    }

}
