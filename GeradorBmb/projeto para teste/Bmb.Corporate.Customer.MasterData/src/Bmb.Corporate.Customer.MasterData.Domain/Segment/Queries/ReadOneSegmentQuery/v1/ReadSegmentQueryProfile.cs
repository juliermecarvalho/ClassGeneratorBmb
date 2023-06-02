using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;
public class ReadSegmentQueryProfile : Profile
{
    public ReadSegmentQueryProfile()
    {
        CreateMap<Entities.v1.Segment, ReadOneSegmentQueryResult>().ReverseMap();
    }
}