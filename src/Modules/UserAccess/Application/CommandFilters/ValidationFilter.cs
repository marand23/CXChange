using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Command;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using FluentValidation;
using MassTransit;
using MassTransit.Internals.GraphValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;



namespace CXChange.Modules.UserAccess.Application.CommandFilters
{

    public class ValidationFilter<T> : IFilter<ConsumeContext<T>> where T : class
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
                Console.WriteLine("validation stopped");
                throw new InvalidCommandException(errors.Select(e => e.ErrorMessage).ToArray());
             

            }
            await next.Send(context);
            Console.WriteLine("validation stoped");
        }
    }
}


