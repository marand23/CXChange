using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Infrastructure.Authentication.AuthenticationHandlers;
using CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore;
using CXChange.Modules.UserAccess.Infrastructure.CustomUserDataProtectionProviders;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserAccess.Infrastructure.Identity;
using CXChange.Modules.UserAccess.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterUserAccessInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserStore<AppUser>, CustomUSerStore>();

            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseSqlServer(
                   connectionString
                    );
            });

            //    services.AddDbContext(typeof(DbContext), typeof(UserManagementDbContext), options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
             .AddDefaultTokenProviders()
             .AddLoginTokenProvider()


                   .AddEntityFrameworkStores<UserManagementDbContext>();
            services.Configure<IdentityOptions>(options =>
            {

                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;




            });
            var a = Assembly.GetExecutingAssembly();
            services.AddAuthentication(CustomAuthOptions.SchemeName)
                .AddScheme<CustomAuthOptions, CustomAuthenticationHandler>(
                CustomAuthOptions.SchemeName, opts =>
                {
                    opts.ExpireTimeSpan = TimeSpan.FromDays(4);
                    opts.SessionStore = new CustomDatabaseTicketStore(services);






                }
                )
               ;
            //services.AddAuthentication(CustomAuthOptions.SchemeName)

            //  .AddScheme<CustomAuthOptions, CustomAuthenticationHandler>(
            //  CustomAuthOptions.SchemeName, opts =>
            //  {
            //      opts.ExpireTimeSpan = TimeSpan.FromDays(4);
            //      opts.SessionStore = new CustomTicketStore(services);
            //      // opts.UserTokenGenerator = ExtensionMethods.GenerateRandomToken(64);





            //  }
            //  )



            // ;

            services.AddScoped<IExchangeUserManager, ExchangeUserManager>();
            services.AddScoped<IExchangeSignInManager, ExchangeSignInManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();









        }
        public static IdentityBuilder AddLoginTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var loginProvider = typeof(LoginTokenProvider<>).MakeGenericType(userType);
            var registerProvider = typeof(RegisterTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("LoginTokenProvider", loginProvider).AddTokenProvider("RegisterTokenProvider", registerProvider);
        }
    }

}
