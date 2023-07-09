using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace TEst
{
    
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentTest(this IServiceCollection services, IWebHostEnvironment environment)
        {



            //AddMAssTransit(services, environment);

        }
        private static void AddMAssTransit(IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddMassTransit(x =>
            {
                // register consumers for module A
                x.AddConsumer<UserRegisteredConsumer>();
                
            });



        }


    }
}
