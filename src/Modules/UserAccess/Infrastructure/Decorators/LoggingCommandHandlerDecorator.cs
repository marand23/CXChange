using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Decorators
{
//    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//where TRequest : IRequest<TResponse>






    //{

    //   ILogger _logger;

    //    public LoggingBehavior(ILogger logger)
    //    {
    //        _logger = logger;
           

    //    }



    //    //(string message, ResultStatus status, string[] errors = null)

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            return await next();
    //        }
    //        catch (BusinessRuleException ex) { 

    //        }
           
    //    }

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        var errors = (await _validator.ValidateAsync(request, cancellationToken)).Errors;

    //        if (errors.Any())
    //        {
    //            //    var a= (TResponse)(typeof(Result<>).MakeGenericType(typeof(TResponse).GetGenericArguments())
    //            //.GetMethod("CreateFailure").Invoke(null));




    //        }
    //        return await next();
    //    }
    //}
}

