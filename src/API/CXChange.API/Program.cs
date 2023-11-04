using CXChange.API;
using CXChange.API.Middlewares;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Validator;
using CXChange.BuildingBlocks.Contract;
using CXChange.BuildingBlocks.Infrastructure;
using CXChange.Modules.Kyc.Application;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.ExternalApiAdapter;
using CXChange.Modules.Kyc.Infrastructure;
using CXChange.Modules.UserAccess.Application;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure;
using FluentValidation;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.HttpOverrides;
using Npgsql;
using Serilog;

Log.Logger = new LoggerConfiguration()
              .WriteTo.Console().MinimumLevel.Information()
              // .WriteTo.Telegram("1136419874:AAEx8Vxxn_9j07zz_MoaeP3k1IotWQ9S2_U", "387581401", period: TimeSpan.FromSeconds(10), includeStackTrace: true, applicationName: "oooop")
              .CreateLogger();

try
{
    NpgsqlConnection.GlobalTypeMapper.MapEnum<KycStatus>("kyc.kyc_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<KycRequestStatus>("kyc.kyc_request_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<StorageTypeEnum>("kyc.kyc_request_storage_type");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("user_access.active_two_factor");
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
    builder.Services.AddHangfire(config =>
    {

        config.UseMemoryStorage();
    });


    builder.Services.AddHangfireServer();


    builder.Services.RegisterUserAccessApplication(builder.Environment);
    builder.Services.RegisterMediator(builder.Environment);
    builder.Services.RegisterCoreElementstApplication();
    builder.Services.RegisterMAssTransit(builder.Environment);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.RegisterExternalApiAdaper();
    builder.Services.RegisterUserAccessInfrastructure(builder.Configuration);
    builder.Services.RegisterKycApplication();
    builder.Services.RegisterBuldingBlocksContract();
    ValidatorOptions.Global.LanguageManager = new CustomMessageManager();



    builder.Services.AddMvc();


    builder.Services.RegisterCoreElementstInfrastructure();
    builder.Services.RegisterKycInfrastructure(builder.Configuration);



    var app = builder.Build();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();

    void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs)
    {

    }
    app.UseHangfireDashboard();
    CXChange.Modules.Kyc.Infrastructure.DependencyInjection.RegisterKycInfrastructureJobs();
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
