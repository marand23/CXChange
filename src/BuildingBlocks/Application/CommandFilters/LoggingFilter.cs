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

                await next.Send(context);

            }
            catch (BusinessRuleException ex)
            {

                throw;
            }
            catch (ExchangeCommandException ex)
            {
                _logger.Error("command failed {message}  {code} CommandBody : {body}", ex.Message, ex.ErrorCode, context.Message);
                throw ex;
            }
            catch (ValidationException ex)
            {
                _logger.Error("validation failed CommandBody : {body}", context.Message);
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

