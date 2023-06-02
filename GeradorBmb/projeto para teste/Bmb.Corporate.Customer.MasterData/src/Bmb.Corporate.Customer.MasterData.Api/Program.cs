using Bmb.Core.Api;
using Bmb.Core.Api.WarmUp;
using Bmb.Core.Domain;
using Bmb.Core.Logger;
using Bmb.Corporate.Customer.MasterData.Infra.Data;
using Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;


var conn = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<BmbContext>(
    x => x.UseSqlServer(conn)
);

builder.Services.AddDomainContext(configuration);
builder.Services.AddInfraData();
builder.Services.AddNotificationContextFromScopedLifeCycle();
builder.Services.AddLogger(configuration);
builder.Services.AddApi(configuration, environment);
//builder.Services.AddApiHealthCheckContext(configuration).AddSqlServerHealthCheckContext(configuration);
//builder.Services.AddWarmUp(configuration).AddSqlServerDbWarmUp();

builder.Host.UseLogger();

var app = builder.Build();
app.UseApi(configuration, environment);
//app.UseApiHealthCheckContext(configuration);
app.UseWarmUp(configuration);

app.Run();