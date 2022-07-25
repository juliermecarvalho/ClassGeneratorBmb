using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;
public class ReadAllSegmentQueryProfile : Profile
{
    public ReadAllSegmentQueryProfile()
    {
        CreateMap<Entities.v1.Segment, ReadAllSegmentQueryResult>().ReverseMap();
    }
}