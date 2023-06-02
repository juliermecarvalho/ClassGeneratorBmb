using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseQueryProfile : Profile
{
    public ReadOneMinhaClasseQueryProfile()
    {
        CreateMap<Entities.v1.MinhaClasse, ReadOneMinhaClasseQueryResult>().ReverseMap();
    }
}
