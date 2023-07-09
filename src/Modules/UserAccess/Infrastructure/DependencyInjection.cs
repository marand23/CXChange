using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserAccess.Infrastructure.Identity;
using CXChange.Modules.UserAccess.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace CXChange.Modules.UserAccess.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
           
           string connectionString =configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseSqlServer(
                   connectionString
                    );
            });

            
            services.AddIdentity<AppUser,IdentityRole>()
             .AddDefaultTokenProviders()
                  

                   .AddEntityFrameworkStores<UserManagementDbContext>();
            services.Configure<IdentityOptions>(options =>
            {

                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;




            });
           
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
            // services.AddScoped(IExchangeSignInManager,ExchangeSignInManager);
            services.AddScoped<IUserRepository, UserRepository>();




           



        }
    }
}
