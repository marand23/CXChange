using CXChange.AdminAPI;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Contract;
using CXChange.BuildingBlocks.Infrastructure;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.ExternalApiAdapter;
using CXChange.Modules.Kyc.Infrastructure;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.UserAccess.Domain.User;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);
NpgsqlConnection.GlobalTypeMapper.MapEnum<KycStatus>("kyc.kyc_status");
NpgsqlConnection.GlobalTypeMapper.MapEnum<KycRequestStatus>("kyc.kyc_request_status");
NpgsqlConnection.GlobalTypeMapper.MapEnum<StorageTypeEnum>("kyc.kyc_request_storage_type");
NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("user_access.active_two_factor");
NpgsqlConnection.GlobalTypeMapper.MapEnum<LandLineStatus>("kyc.kyc_land_line_status");
NpgsqlConnection.GlobalTypeMapper.MapEnum<BankCardStatus>("payment.bankcard_status");

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApi();

builder.Services.RegisterMAssTransit(builder.Environment);
builder.Services.RegisterMediator(builder.Environment);
builder.Services.RegisterCoreElementstApplication();
builder.Services.RegisterCoreElementstInfrastructure();
builder.Services.RegisterKycInfrastructure(builder.Configuration);
builder.Services.RegisterCoreElementstApplication();
builder.Services.RegisterBuldingBlocksContract();
builder.Services.RegisterExternalApiAdaper();


// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseHttpsRedirection();

app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
public class zibal
{
    public string Purchase { get; set; }
    public string Verify { get; set; }
    public string CallbackUrl { get; set; }
}