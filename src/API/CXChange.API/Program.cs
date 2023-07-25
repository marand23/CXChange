using CXChange.Modules.UserAccess.Infrastructure;
using CXChange.Modules.UserAccess.Application;
using CXChange.CoreElements.Infrastructure;

using System;
using Microsoft.IdentityModel.Tokens;
using MassTransit;
using CXChange.API;
using MassTransit.Mediator;
using CXChange.BuildingBlocks.Application;

using MediatR;
using System.Reflection;
using CXChange.Modules.UserAccess.Infrastructure.Decorators;
using Serilog;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using CXChange.API.Modules.UserManagment;
using CXChange.API.Middlewares;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using Azure;
using CXChange.Modules.UserAccess.Application.CommandFilters;
using CXChange.BuildingBlocks.Application.Command;
using Microsoft.AspNetCore.Mvc;
using CXChange.Modules.UserAccess.Application.Commands.UserLogin;
using MassTransit.Middleware;
using static MassTransit.Logging.OperationName;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.API.Extensions;

Log.Logger = new LoggerConfiguration()
              .WriteTo.Console().MinimumLevel.Warning()
             // .WriteTo.Telegram("1136419874:AAEx8Vxxn_9j07zz_MoaeP3k1IotWQ9S2_U", "387581401", period: TimeSpan.FromSeconds(10), includeStackTrace: true, applicationName: "oooop")
              .CreateLogger();

try
{
    Log.Information("Starting web application");
   
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    builder.Services.AddSingleton(Log.Logger);



    // Add services to the container.

    builder.Services.AddControllers(options =>
    {
        
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.RegisterUserManagmentInfrastructure(builder.Configuration);

    builder.Services.RegisterUserManagmentApplication(builder.Environment);

    builder.Services.RegisterCoreElementstInfrastructure();
    builder.Services.RegisterAPI(builder.Environment);
    builder.Services.Configure<ApiBehaviorOptions>(o =>
    {
     
   
        o.InvalidModelStateResponseFactory = actionContext =>
            new UnprocessableEntityObjectResult(new ApiValidationErrorModel { message = "the request is not valid",errors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(v=>v.ErrorMessage).ToArray(), success = false }); ;
    });

    builder.Services.AddMediator(cfg =>
    {

        cfg.AddConsumer<LoginHandler>();
        cfg.AddConsumer<RegisterUserHandler>();
        cfg.AddConsumer<UserRegisteredDomainEventHandler>();
        cfg.AddConsumer<LoginHandler>();
        
        cfg.ConfigureMediator((context, mcfg) =>
        {
            //mcfg.UseConsumeFilter(typeof(ValidationFilter<>), context);
            //mcfg.UseConsumeFilter(typeof(LoggingFilter<>), context);
            mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context);

        });




    });
    

    var app = builder.Build();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
