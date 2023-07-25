using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application;
using FluentValidation;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Application.CommandFilters
{
  
    public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T :class
    {
        private Serilog.ILogger _logger;
        
        public LoggingFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void Probe(ProbeContext context)
        {
        }


        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {

            try
            {
                Console.WriteLine("logging filter started");

                await next.Send(context);
                Console.WriteLine("logging filter stopped");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
   
