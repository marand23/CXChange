using CXChange.BuildingBlocks.Application.Command;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.CommandFilters
{

    public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class, ICommandBase
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

