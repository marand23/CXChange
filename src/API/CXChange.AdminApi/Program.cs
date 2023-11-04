using CXChange.AdminAPI;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Infrastructure;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.Infrastructure;
using CXChange.Modules.UserAccess.Domain.User;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);
NpgsqlConnection.GlobalTypeMapper.MapEnum<KycStatus>("kyc.kyc_status");
NpgsqlConnection.GlobalTypeMapper.MapEnum<KycRequestStatus>("kyc.kyc_request_status");
NpgsqlConnection.GlobalTypeMapper.MapEnum<StorageTypeEnum>("kyc.kyc_request_storage_type");
NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("user_access.active_two_factor");
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApi();

builder.Services.RegisterMAssTransit(builder.Environment);
builder.Services.RegisterMediator(builder.Environment);
builder.Services.RegisterCoreElementstApplication();
builder.Services.RegisterCoreElementstInfrastructure();
builder.Services.RegisterKycInfrastructure(builder.Configuration);
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
