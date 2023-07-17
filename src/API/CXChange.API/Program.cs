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

var builder = WebApplication.CreateBuilder(args);

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
    builder.Services.RegisterUserManagmentApplication(builder.Environment,cfg);
});
builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));
builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingCommandHandlerDecorator<,>));
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("log.txt", rollingInterval: RollingInterval.Hour)
           
           .CreateLogger();





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
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDecorator<TService, TDecorator>(this IServiceCollection services)
        where TService : class
        where TDecorator : class, TService
    {
        services.AddTransient<TDecorator>();
        services.AddTransient<TService>(sp => sp.GetRequiredService<TDecorator>());
        return services;
    }
}