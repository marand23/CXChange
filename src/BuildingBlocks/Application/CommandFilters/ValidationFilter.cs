using CXChange.BuildingBlocks.Application.Error;
using CXChange.BuildingBlocks.Application.MessageInterfaces;
using FluentValidation;
using MassTransit;
using Serilog;



namespace CXChange.BuildingBlocks.Application.CommandFilters
{

    public class ValidationFilter<T> : IFilter<ConsumeContext<T>> where T : class, IValidatable
    {
        private IValidator<T> _validator;
        ILogger _logger;
        public ValidationFilter(IValidator<T> validator, ILogger logger)
        {
            _validator = validator;
            _logger = logger;
        }
        public void Probe(ProbeContext context)
        {
        }


        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {


            var result = await _validator.ValidateAsync(context.Message);
            var errors = result.Errors
              .Where(error => error != null).ToList();

            if (!result.IsValid)
            {
                _logger.Error("Validation Failed Command Body : {body} ", context.Message);
                await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<ValidationFilter<T>>.ShortName);


                await context.RespondAsync(Result.ValidationFailed(new ValidationError(errors)));


                return;

            }
            else
            {
                await next.Send(context);

            }


        }

    }
}


