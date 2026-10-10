using CXChange.API;
using CXChange.API.Middlewares;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Validator;
using CXChange.BuildingBlocks.Contract;
using CXChange.BuildingBlocks.Infrastructure;
using CXChange.Modules.Kyc.Application;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.ExternalApiAdapter;
using CXChange.Modules.Kyc.Infrastructure;
using CXChange.Modules.Payment.Application;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.Deposit.Enums;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using CXChange.Modules.Payment.Infrastructure;
using CXChange.Modules.Payment.PaymentApiAdapters;
using CXChange.Modules.UserAccess.Application;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure;
using FluentValidation;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.HttpOverrides;
using Npgsql;
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
              .WriteTo.Console().MinimumLevel.Warning()
           //.WriteTo.Telegram("1136419874:AAEx8Vxxn_9j07zz_MoaeP3k1IotWQ9S2_U", "387581401", period: TimeSpan.FromSeconds(10), includeStackTrace: true, applicationName: "oooop")
           .WriteTo.File(new JsonFormatter(), rollingInterval: RollingInterval.Day, path: @$"c:\test_project\ApiLogsTest\{DateTime.Now.ToShortDateString()}.json")
              .CreateLogger();

try
{
    NpgsqlConnection.GlobalTypeMapper.MapEnum<KycStatus>("kyc.kyc_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<KycRequestStatus>("kyc.kyc_request_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<StorageTypeEnum>("kyc.kyc_request_storage_type");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("user_access.active_two_factor");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<LandLineStatus>("kyc.kyc_land_line_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<BankCardStatus>("payment.bankcard_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<ShabaStatus>("payment.shaba_status");
    NpgsqlConnection.GlobalTypeMapper.MapEnum<DepositStatus>("payment.deposit_status");
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
    builder.Services.RegisterPaymentInfrastructure(builder.Configuration);
    builder.Services.RegisterPaymentApplication();

    ValidatorOptions.Global.LanguageManager = new CustomMessageManager();



    builder.Services.AddMvc();


    builder.Services.RegisterCoreElementstInfrastructure();
    builder.Services.RegisterKycInfrastructure(builder.Configuration);
    builder.Services.RegisterPaymentApiAdapters(builder.Configuration);


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
