using Bmb.Core.Api;
using Bmb.Core.Api.HealthCheck;
using Bmb.Core.Api.WarmUp;
using Bmb.Core.Domain;
using Bmb.Core.Logger;
using Bmb.Teste.Operation.MasterData.Infra.Data;
using Bmb.Teste.Operation.MasterData.Infra.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;
var services = builder.Services;

builder.Host.UseLogger();

services.AddApi(configuration, environment);
services.AddDomainContext(configuration);
services.AddNotificationContextFromScopedLifeCycle();
services.AddLogger(configuration);
services.AddApiHealthCheckContext(configuration);
services.AddWarmUp(configuration);

services.AddInfraData();
services.AddDbContext<BmbContext>();

var app = builder.Build();

app.UseApi(configuration, environment);
app.UseApiHealthCheckContext(configuration);
app.UseWarmUp(configuration);

app.Run();