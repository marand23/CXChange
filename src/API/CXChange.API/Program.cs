using CXChange.Modules.UserAccess.Infrastructure;
using CXChange.Modules.UserAccess.Application;
using CXChange.CoreElements.Infrastructure;

using System;
using Microsoft.IdentityModel.Tokens;
using MassTransit;
using CXChange.API;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterUserManagmentInfrastructure(builder.Configuration);
builder.Services.RegisterUserManagmentApplication(builder.Environment);
builder.Services.RegisterCoreElementstInfrastructure();
builder.Services.RegisterAPI(builder.Environment);



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
