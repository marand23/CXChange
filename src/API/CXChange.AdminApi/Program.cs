using CXChange.AdminAPI;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApi();

builder.Services.RegisterMAssTransit(builder.Environment);
builder.Services.RegisterMediator(builder.Environment);
builder.Services.RegisterCoreElementstApplication();
builder.Services.RegisterCoreElementstInfrastructure();

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
