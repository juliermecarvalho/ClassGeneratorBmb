﻿using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Infra.Data.Repositories.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data;

public static class Bootstrapper
{
    public static void AddInfraData(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISegmentRepository, SegmentRepository>();
    }
}