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

Log.Logger = new LoggerConfiguration()
              .WriteTo.Console()
             // .WriteTo.Telegram("1136419874:AAEx8Vxxn_9j07zz_MoaeP3k1IotWQ9S2_U", "387581401", period: TimeSpan.FromSeconds(10), includeStackTrace: true, applicationName: "oooop")
              .CreateLogger();

try
{
    Log.Information("Starting web application");
   
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    builder.Services.AddSingleton(Log.Logger);
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());



    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.RegisterUserManagmentInfrastructure(builder.Configuration);


    builder.Services.RegisterCoreElementstInfrastructure();
    builder.Services.RegisterAPI(builder.Environment);

    builder.Services.AddMediatR(cfg =>
    {
        builder.Services.RegisterUserManagmentApplication(builder.Environment, cfg);
    });
    builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));
    builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingCommandHandlerDecorator<,>));
   



    var app = builder.Build();

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
