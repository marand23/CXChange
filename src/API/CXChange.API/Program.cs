using CXChange.API;
using CXChange.API.Middlewares;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Validator;
using CXChange.CoreElements.Infrastructure;
using CXChange.Modules.UserAccess.Application;
using CXChange.Modules.UserAccess.Infrastructure;
using FluentValidation;
using Serilog;

Log.Logger = new LoggerConfiguration()
              .WriteTo.Console().MinimumLevel.Information()
              // .WriteTo.Telegram("1136419874:AAEx8Vxxn_9j07zz_MoaeP3k1IotWQ9S2_U", "387581401", period: TimeSpan.FromSeconds(10), includeStackTrace: true, applicationName: "oooop")
              .CreateLogger();

try
{

    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    builder.Services.AddSingleton(Log.Logger);
    builder.Services.AddControllers(options =>
    {

    });
    builder.Services.RegisterAPI(opt =>
    {
        opt.Url = "exchange";
    });
    builder.Services.RegisterUserAccessApplication(builder.Environment);
    builder.Services.RegisterMediator(builder.Environment);
    builder.Services.RegisterCoreElementstApplication();
    builder.Services.RegisterMAssTransit(builder.Environment);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.RegisterUserAccessInfrastructure(builder.Configuration);
    ValidatorOptions.Global.LanguageManager = new CustomMessageManager();



    builder.Services.RegisterCoreElementstInfrastructure();




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
