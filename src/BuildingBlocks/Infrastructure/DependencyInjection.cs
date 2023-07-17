using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.CoreElements.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstInfrastructure(this IServiceCollection servicesn)
        {
            servicesn.AddScoped<ITotp, Totp.Totp>();
            
        }
    }
}
