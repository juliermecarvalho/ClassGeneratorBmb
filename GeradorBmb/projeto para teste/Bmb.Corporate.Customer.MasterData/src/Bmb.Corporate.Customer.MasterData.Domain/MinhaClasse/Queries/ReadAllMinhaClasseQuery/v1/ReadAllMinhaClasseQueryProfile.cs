using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;

public class ReadAllMinhaClasseCommandProfile : Profile
{
    public ReadAllMinhaClasseCommandProfile()
    {
        CreateMap<Entities.v1.MinhaClasse, ReadAllMinhaClasseQueryResult>().ReverseMap();
    }
}
