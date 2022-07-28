using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Bmb.Teste.Operation.MasterData.Infra.Data.Repositories.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Bmb.Teste.Operation.MasterData.Infra.Data;

public static class Bootstrapper
{
    public static void AddInfraData(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExampleRepository, ExampleRepository>();
    }
}