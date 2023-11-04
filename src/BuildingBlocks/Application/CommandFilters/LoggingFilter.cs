using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using MassTransit;

namespace CXChange.BuildingBlocks.Application.CommandFilters
{

    public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
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
            catch (BusinessRuleException ex)
            {
                var a = context.GetType();
                _logger.Error($"Exception occured in{context.SourceAddress}");
                _logger.Error(ex.Message);
                // _logger.Error(ex.StackTrace);
                throw;
            }
            catch (ExchangeCommandException ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

