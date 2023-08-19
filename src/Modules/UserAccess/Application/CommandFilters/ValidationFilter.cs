using CXChange.BuildingBlocks.Application.MessageInterfaces;
using FluentValidation;
using MassTransit;



namespace CXChange.Modules.UserAccess.Application.CommandFilters
{

    public class ValidationFilter<T> : IFilter<ConsumeContext<T>> where T : class, IValidatable
    {
        private IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }
        public void Probe(ProbeContext context)
        {
        }


        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            Console.WriteLine("validation started");
            var result = await _validator.ValidateAsync(context.Message);
            var errors = result.Errors
              .Where(error => error != null)
              .ToList();
            if (!result.IsValid)
            {
                throw new BuildingBlocks.Application.Exceptions.ValidationException(errors.GroupBy(x => x.PropertyName).Select(x => x.FirstOrDefault()).ToList());




            }
            else
            {
                await next.Send(context);
                Console.WriteLine("validation stoped");
            }

        }
    }
}


